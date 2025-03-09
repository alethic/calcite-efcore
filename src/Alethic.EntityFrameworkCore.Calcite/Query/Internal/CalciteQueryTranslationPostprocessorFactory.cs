using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteQueryTranslationPostprocessorFactory : RelationalQueryTranslationPostprocessorFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteQueryTranslationPostprocessorFactory(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

    }

}
