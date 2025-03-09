using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteSqlTranslatingExpressionVisitorFactory : RelationalSqlTranslatingExpressionVisitorFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteSqlTranslatingExpressionVisitorFactory(RelationalSqlTranslatingExpressionVisitorDependencies dependencies) :
            base(dependencies)
        {

        }

        /// <inheritdoc/>
        public override RelationalSqlTranslatingExpressionVisitor Create(QueryCompilationContext queryCompilationContext, QueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor)
        {
            return base.Create(queryCompilationContext, queryableMethodTranslatingExpressionVisitor);
        }

    }

}
