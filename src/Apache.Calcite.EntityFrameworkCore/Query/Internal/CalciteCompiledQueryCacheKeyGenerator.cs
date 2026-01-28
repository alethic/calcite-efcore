using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
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
