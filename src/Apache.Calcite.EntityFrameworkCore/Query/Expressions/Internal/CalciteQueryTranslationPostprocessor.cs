using Apache.Calcite.EntityFrameworkCore.Query.Internal;

using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Expressions.Internal
{

    /// <inheritdoc />
    public class CalciteQueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="queryCompilationContext"></param>
        public CalciteQueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies, CalciteQueryCompilationContext queryCompilationContext) : 
            base(dependencies, relationalDependencies, queryCompilationContext)
        {

        }

    }

}
