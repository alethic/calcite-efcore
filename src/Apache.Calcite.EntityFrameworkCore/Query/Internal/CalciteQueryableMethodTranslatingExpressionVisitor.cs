using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryableMethodTranslatingExpressionVisitor : RelationalQueryableMethodTranslatingExpressionVisitor
    {

        readonly RelationalQueryCompilationContext _queryCompilationContext;
        readonly bool _subquery;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="queryCompilationContext"></param>
        public CalciteQueryableMethodTranslatingExpressionVisitor(QueryableMethodTranslatingExpressionVisitorDependencies dependencies, RelationalQueryableMethodTranslatingExpressionVisitorDependencies relationalDependencies, RelationalQueryCompilationContext queryCompilationContext) :
            base(dependencies, relationalDependencies, queryCompilationContext)
        {
            _queryCompilationContext = queryCompilationContext;
            _subquery = false;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parentVisitor"></param>
        protected CalciteQueryableMethodTranslatingExpressionVisitor(CalciteQueryableMethodTranslatingExpressionVisitor parentVisitor) :
            base(parentVisitor)
        {
            _queryCompilationContext = parentVisitor._queryCompilationContext;
            _subquery = true;
        }

        /// <inheritdoc/>
        protected override QueryableMethodTranslatingExpressionVisitor CreateSubqueryVisitor()
        {
            return new CalciteQueryableMethodTranslatingExpressionVisitor(this);
        }

        /// <inheritdoc/>
        protected override SelectExpression CreateSelect(IEntityType entityType)
        {
            return base.CreateSelect(entityType);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Calcite's <c>COUNT(*)</c> always returns <c>BIGINT</c> (<see cref="long"/>). EF Core's base implementation shapes the result as
        /// <see cref="int"/>, which causes an <see cref="System.InvalidCastException"/> at read time. This override delegates to
        /// <see cref="RelationalQueryableMethodTranslatingExpressionVisitor.TranslateLongCount"/> so the shaper reads a <c>long</c>, then
        /// wraps it in a checked <c>(int)</c> conversion to satisfy the <c>Count()</c> return type contract.
        /// </remarks>
        protected override ShapedQueryExpression? TranslateCount(ShapedQueryExpression source, LambdaExpression? predicate)
        {
            var longResult = TranslateLongCount(source, predicate);
            if (longResult is null)
                return null;

            // Rewrite the shaper from `long` -> `int` so the compiled query reads the correct CLR type.
            var longShaper = longResult.ShaperExpression;
            var intShaper = Expression.Convert(longShaper, typeof(int));
            return longResult.UpdateShaperExpression(intShaper);
        }

    }

}
