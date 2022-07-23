using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Expressions.Internal
{

    /// <inheritdoc />
    public class CalciteQueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {

        /// <inheritdoc />
        public CalciteQueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies, QueryCompilationContext queryCompilationContext) : 
            base(dependencies, relationalDependencies, queryCompilationContext)
        {

        }

    }

}
