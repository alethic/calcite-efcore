using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteQueryCompilationContext : RelationalQueryCompilationContext
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="async"></param>
        public CalciteQueryCompilationContext(QueryCompilationContextDependencies dependencies, RelationalQueryCompilationContextDependencies relationalDependencies, bool async) :
            base(dependencies, relationalDependencies, async)
        {

        }

    }

}
