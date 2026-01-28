using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteEvaluatableExpressionFilter : RelationalEvaluatableExpressionFilter
    {

        /// <summary>
        /// Initializes a new instane.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteEvaluatableExpressionFilter(EvaluatableExpressionFilterDependencies dependencies, RelationalEvaluatableExpressionFilterDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

    }

}
