using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteSqlTranslatingExpressionVisitorFactory : IRelationalSqlTranslatingExpressionVisitorFactory
    {

        readonly RelationalSqlTranslatingExpressionVisitorDependencies _dependencies;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteSqlTranslatingExpressionVisitorFactory(RelationalSqlTranslatingExpressionVisitorDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        /// <summary>
        /// Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalSqlTranslatingExpressionVisitorDependencies Dependencies => _dependencies;

        public virtual RelationalSqlTranslatingExpressionVisitor Create(QueryCompilationContext queryCompilationContext, QueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor)
            => new CalciteSqlTranslatingExpressionVisitor(Dependencies, queryCompilationContext, queryableMethodTranslatingExpressionVisitor);

    }

}
