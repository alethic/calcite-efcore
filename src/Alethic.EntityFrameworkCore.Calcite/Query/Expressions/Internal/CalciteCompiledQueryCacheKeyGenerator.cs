using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Expressions.Internal
{

    /// <inheritdoc />
    public class CalciteCompiledQueryCacheKeyGenerator : RelationalCompiledQueryCacheKeyGenerator
    {

        /// <inheritdoc />
        public CalciteCompiledQueryCacheKeyGenerator(CompiledQueryCacheKeyGeneratorDependencies dependencies, RelationalCompiledQueryCacheKeyGeneratorDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

    }

}
