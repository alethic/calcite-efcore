using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteSqlTranslatingExpressionVisitor : RelationalSqlTranslatingExpressionVisitor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="queryCompilationContext"></param>
        /// <param name="queryableMethodTranslatingExpressionVisitor"></param>
        public CalciteSqlTranslatingExpressionVisitor(RelationalSqlTranslatingExpressionVisitorDependencies dependencies, QueryCompilationContext queryCompilationContext, QueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor) :
            base(dependencies, queryCompilationContext, queryableMethodTranslatingExpressionVisitor)
        {

        }

    }

}
