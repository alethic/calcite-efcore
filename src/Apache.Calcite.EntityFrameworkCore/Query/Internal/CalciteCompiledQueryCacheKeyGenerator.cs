using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteCompiledQueryCacheKeyGenerator(CompiledQueryCacheKeyGeneratorDependencies dependencies, RelationalCompiledQueryCacheKeyGeneratorDependencies relationalDependencies) :
        RelationalCompiledQueryCacheKeyGenerator(dependencies, relationalDependencies)
    {



    }

}
