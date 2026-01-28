using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryableMethodTranslatingExpressionVisitor : RelationalQueryableMethodTranslatingExpressionVisitor
    {

        protected readonly RelationalQueryCompilationContext _queryCompilationContext;
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
        protected CalciteQueryableMethodTranslatingExpressionVisitor(CalciteQueryableMethodTranslatingExpressionVisitor parentVisitor)
            : base(parentVisitor)
        {
            _queryCompilationContext = parentVisitor._queryCompilationContext;
            _subquery = true;
        }

        protected override QueryableMethodTranslatingExpressionVisitor CreateSubqueryVisitor()
            => new CalciteQueryableMethodTranslatingExpressionVisitor(this);

    }

}
