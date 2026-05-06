using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.Metadata;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    /// <inheritdoc />
    public class CalciteEntitySequenceHiLoValueGenerator<TValue> : HiLoValueGenerator<TValue>
    {

        readonly ICalciteEntitySequence _sequence;
        readonly ICurrentDbContext _currentDbContext;
        readonly IRelationalCommandDiagnosticsLogger _commandLogger;
        readonly MethodInfo _getNextSyncMethod;
        readonly MethodInfo _getNextAsyncMethod;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="currentDbContext"></param>
        /// <param name="generatorState"></param>
        /// <param name="commandLogger"></param>
        public CalciteEntitySequenceHiLoValueGenerator(ICurrentDbContext currentDbContext, CalciteEntitySequenceGeneratorState generatorState, IRelationalCommandDiagnosticsLogger commandLogger) :
            base(generatorState)
        {
            _sequence = generatorState.EntitySequence;
            _currentDbContext = currentDbContext;
            _commandLogger = commandLogger;

            var entityClrType = _sequence.EntityType.ClrType;

            _getNextSyncMethod = typeof(CalciteEntitySequenceHiLoValueGenerator<TValue>)
                .GetMethod(nameof(GetNextValueCore), BindingFlags.NonPublic | BindingFlags.Instance)!
                .MakeGenericMethod(entityClrType);

            _getNextAsyncMethod = typeof(CalciteEntitySequenceHiLoValueGenerator<TValue>)
                .GetMethod(nameof(GetNextValueCoreAsync), BindingFlags.NonPublic | BindingFlags.Instance)!
                .MakeGenericMethod(entityClrType);
        }

        /// <inheritdoc/>
        protected override long GetNewLowValue()
            => (long)_getNextSyncMethod.Invoke(this, null)!;

        /// <inheritdoc/>
        protected override async Task<long> GetNewLowValueAsync(CancellationToken cancellationToken = default)
            => await ((Task<long>)_getNextAsyncMethod.Invoke(this, [cancellationToken])!).ConfigureAwait(false);

        /// <summary>
        /// Gets the next low value by atomically incrementing the entity sequence row
        /// and reading back the new value within a serializable transaction.
        /// </summary>
        private long GetNextValueCore<TEntity>() where TEntity : class
        {
            var context = _currentDbContext.Context;
            var query = BuildFilteredQuery<TEntity>(context);

            using var transaction = context.Database.CurrentTransaction is null
                ? context.Database.BeginTransaction(IsolationLevel.Serializable)
                : null;

            query.ExecuteUpdate(BuildSetPropertyAction<TEntity>());
            var newValue = query.Select(BuildValueSelector<TEntity>()).Single();

            transaction?.Commit();

            return newValue;
        }

        /// <summary>
        /// Async version of <see cref="GetNextValueCore{TEntity}"/>
        /// </summary>
        private async Task<long> GetNextValueCoreAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
        {
            var context = _currentDbContext.Context;
            var query = BuildFilteredQuery<TEntity>(context);

            var transaction = context.Database.CurrentTransaction is null
                ? await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false)
                : null;

            try
            {
                await query.ExecuteUpdateAsync(BuildSetPropertyAction<TEntity>(), cancellationToken).ConfigureAwait(false);
                var newValue = await query.Select(BuildValueSelector<TEntity>()).SingleAsync(cancellationToken).ConfigureAwait(false);

                if (transaction is not null)
                    await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                return newValue;
            }
            finally
            {
                if (transaction is not null)
                    await transaction.DisposeAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Builds an <see cref="IQueryable{TEntity}"/> filtered to the sequence row.
        /// </summary>
        private IQueryable<TEntity> BuildFilteredQuery<TEntity>(DbContext context) where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (_sequence.EntityFilter is Expression<Func<TEntity, bool>> filter)
                query = query.Where(filter);

            return query;
        }

        /// <summary>
        /// Builds <c>e => (long)e.ValueProperty</c>.
        /// </summary>
        private Expression<Func<TEntity, long>> BuildValueSelector<TEntity>()
        {
            var param = Expression.Parameter(typeof(TEntity), "e");
            var property = typeof(TEntity).GetProperty(_sequence.ValueProperty!.Name)!;
            var access = Expression.MakeMemberAccess(param, property);
            return Expression.Lambda<Func<TEntity, long>>(Expression.Convert(access, typeof(long)), param);
        }

        /// <summary>
        /// Builds an <see cref="Action{T}"/> that calls
        /// <c>SetProperty(e => e.ValueProp, e => (TProp)((long)e.ValueProp + incrementBy))</c>
        /// on the <see cref="UpdateSettersBuilder{TEntity}"/>
        /// </summary>
        private Action<UpdateSettersBuilder<TEntity>> BuildSetPropertyAction<TEntity>() where TEntity : class
        {
            var property = typeof(TEntity).GetProperty(_sequence.ValueProperty!.Name)!;
            var propertyType = property.PropertyType;
            var funcType = typeof(Func<,>).MakeGenericType(typeof(TEntity), propertyType);

            // Expression<Func<TEntity, TProp>>: e => e.ValueProp
            var selectorParam = Expression.Parameter(typeof(TEntity), "e");
            var selectorAccess = Expression.MakeMemberAccess(selectorParam, property);
            var propSelector = Expression.Lambda(funcType, selectorAccess, selectorParam);

            // Expression<Func<TEntity, TProp>>: e => (TProp)((long)e.ValueProp + incrementBy)
            var factoryParam = Expression.Parameter(typeof(TEntity), "e");
            var factoryAccess = Expression.MakeMemberAccess(factoryParam, property);
            var incremented = Expression.Convert(
                Expression.Add(
                    Expression.Convert(factoryAccess, typeof(long)),
                    Expression.Constant((long)_sequence.IncrementBy)),
                propertyType);
            var valueFactory = Expression.Lambda(funcType, incremented, factoryParam);

            // Resolve SetProperty<TProp>(Expression<Func<TEntity, TProp>>, Expression<Func<TEntity, TProp>>)
            var setPropertyMethod = typeof(UpdateSettersBuilder<TEntity>)
                .GetMethods()
                .First(m => m.Name == nameof(UpdateSettersBuilder<TEntity>.SetProperty)
                    && m.IsGenericMethodDefinition
                    && m.GetParameters().Length == 2
                    && m.GetParameters()[1].ParameterType.IsGenericType
                    && m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>))
                .MakeGenericMethod(propertyType);

            return builder => setPropertyMethod.Invoke(builder, [propSelector, valueFactory]);
        }

        /// <inheritdoc/>
        public override bool GeneratesTemporaryValues => false;

    }

}
