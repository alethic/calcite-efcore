using System;
using System.ComponentModel;

using Alethic.EntityFrameworkCore.Calcite.Diagnostics.Internal;
using Alethic.EntityFrameworkCore.Calcite.Infrastructure;
using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;
using Alethic.EntityFrameworkCore.Calcite.Metadata.Conventions;
using Alethic.EntityFrameworkCore.Calcite.Metadata.Internal;
using Alethic.EntityFrameworkCore.Calcite.Query;
using Alethic.EntityFrameworkCore.Calcite.Query.Expressions.Internal;
using Alethic.EntityFrameworkCore.Calcite.Query.Internal;
using Alethic.EntityFrameworkCore.Calcite.Storage.Internal;
using Alethic.EntityFrameworkCore.Calcite.Update.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions
{

    public static class CalciteServiceCollectionExtensions
    {

        /// <summary>
        /// Registers the given Entity Framework <see cref="DbContext" /> as a service in the <see cref="IServiceCollection" />
        /// and configures it to connect to Calcite.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method is a shortcut for configuring a <see cref="DbContext" /> to use Calcite. It does not support all options.
        ///         Use <see cref="O:EntityFrameworkServiceCollectionExtensions.AddDbContext" /> and related methods for full control of
        ///         this process.
        ///     </para>
        ///     <para>
        ///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
        ///         For applications that don't use dependency injection, consider creating <see cref="DbContext" />
        ///         instances directly with its constructor. The <see cref="DbContext.OnConfiguring" /> method can then be
        ///         overridden to configure the SQLite provider and connection string.
        ///     </para>
        ///     <para>
        ///         To configure the <see cref="DbContextOptions{TContext}" /> for the context, either override the
        ///         <see cref="DbContext.OnConfiguring" /> method in your derived context, or supply
        ///         an optional action to configure the <see cref="DbContextOptions" /> for the context.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <typeparam name="TContext">The type of context to be registered.</typeparam>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="connectionString">The connection string of the database to connect to.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional Calcite specific configuration.</param>
        /// <param name="optionsAction">An optional action to configure the <see cref="DbContextOptions" /> for the context.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddCalcite<TContext>(
            this IServiceCollection serviceCollection,
            string? connectionString,
            Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null,
            Action<DbContextOptionsBuilder>? optionsAction = null)
            where TContext : DbContext
            => serviceCollection.AddDbContext<TContext>(
                (_, options) =>
                {
                    optionsAction?.Invoke(options);
                    options.UseCalcite(connectionString, calciteOptionsAction);
                });

        /// <summary>
        ///     <para>
        ///         Adds the services required by the Calciate provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />.
        ///     </para>
        ///     <para>
        ///         Warning: Do not call this method accidentally. It is much more likely you need
        ///         to call <see cref="AddCalcite{TContext}" />.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     Calling this method is no longer necessary when building most applications, including those that
        ///     use dependency injection in ASP.NET or elsewhere.
        ///     It is only needed when building the internal service provider for use with
        ///     the <see cref="DbContextOptionsBuilder.UseInternalServiceProvider" /> method.
        ///     This is not recommend other than for some advanced scenarios.
        /// </remarks>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IServiceCollection AddEntityFrameworkCalcite(this IServiceCollection serviceCollection)
        {
            var builder = new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<LoggingDefinitions, CalciteLoggingDefinitions>()
                .TryAdd<IDatabaseProvider, DatabaseProvider<CalciteOptionsExtension>>()
                .TryAdd<IRelationalTypeMappingSource, CalciteTypeMappingSource>()
                .TryAdd<ISqlGenerationHelper, CalciteSqlGenerationHelper>()
                .TryAdd<IRelationalAnnotationProvider, CalciteAnnotationProvider>()
                .TryAdd<IModelValidator, CalciteModelValidator>()
                .TryAdd<IProviderConventionSetBuilder, CalciteConventionSetBuilder>()
                .TryAdd<IModificationCommandBatchFactory, CalciteModificationCommandBatchFactory>()
                .TryAdd<IModificationCommandFactory, CalciteModificationCommandFactory>()
                .TryAdd<IRelationalConnection>(p => p.GetRequiredService<ICalciteRelationalConnection>())
                .TryAdd<IRelationalQueryStringFactory, CalciteQueryStringFactory>()
                .TryAdd<IQueryCompilationContextFactory, CalciteQueryCompilationContextFactory>()
                .TryAdd<IMethodCallTranslatorProvider, CalciteMethodCallTranslatorProvider>()
                .TryAdd<IAggregateMethodCallTranslatorProvider, CalciteAggregateMethodCallTranslatorProvider>()
                .TryAdd<IMemberTranslatorProvider, CalciteMemberTranslatorProvider>()
                .TryAdd<IQuerySqlGeneratorFactory, CalciteQuerySqlGeneratorFactory>()
                .TryAdd<IQueryableMethodTranslatingExpressionVisitorFactory, CalciteQueryableMethodTranslatingExpressionVisitorFactory>()
                .TryAdd<IRelationalSqlTranslatingExpressionVisitorFactory, CalciteSqlTranslatingExpressionVisitorFactory>()
                .TryAdd<IQueryTranslationPostprocessorFactory, CalciteQueryTranslationPostprocessorFactory>()
                .TryAdd<ISqlExpressionFactory, CalciteSqlExpressionFactory>()
                .TryAdd<IRelationalParameterBasedSqlProcessorFactory, CalciteParameterBasedSqlProcessorFactory>()
                .TryAddProviderSpecificServices(b => b.TryAddScoped<ICalciteRelationalConnection, CalciteRelationalConnection>())
                .TryAddCoreServices();

            return serviceCollection;
        }

    }

}
