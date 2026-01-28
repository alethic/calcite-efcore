using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryableMethodTranslatingExpressionVisitorFactory(QueryableMethodTranslatingExpressionVisitorDependencies dependencies, RelationalQueryableMethodTranslatingExpressionVisitorDependencies relationalDependencies) : IQueryableMethodTranslatingExpressionVisitorFactory
    {

        /// <summary>
        /// Dependencies for this service.
        /// </summary>
        protected virtual QueryableMethodTranslatingExpressionVisitorDependencies Dependencies { get; } = dependencies;

        /// <summary>
        /// Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalQueryableMethodTranslatingExpressionVisitorDependencies RelationalDependencies { get; } = relationalDependencies;

        public virtual QueryableMethodTranslatingExpressionVisitor Create(QueryCompilationContext queryCompilationContext)
            => new CalciteQueryableMethodTranslatingExpressionVisitor(Dependencies, RelationalDependencies, (RelationalQueryCompilationContext)queryCompilationContext);
    }

}
