using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteQueryableMethodTranslatingExpressionVisitorFactory : RelationalQueryableMethodTranslatingExpressionVisitorFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteQueryableMethodTranslatingExpressionVisitorFactory(QueryableMethodTranslatingExpressionVisitorDependencies dependencies, RelationalQueryableMethodTranslatingExpressionVisitorDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

        /// <inheritdoc/>
        public override QueryableMethodTranslatingExpressionVisitor Create(QueryCompilationContext queryCompilationContext)
        {
            return new CalciteQueryableMethodTranslatingExpressionVisitor(Dependencies, RelationalDependencies, (CalciteQueryCompilationContext)queryCompilationContext);
        }

    }

}
