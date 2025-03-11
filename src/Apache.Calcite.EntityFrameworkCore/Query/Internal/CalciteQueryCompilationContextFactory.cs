using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryCompilationContextFactory : RelationalQueryCompilationContextFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteQueryCompilationContextFactory(QueryCompilationContextDependencies dependencies, RelationalQueryCompilationContextDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

        /// <inheritdoc/>
        public override QueryCompilationContext Create(bool async)
        {
            return new CalciteQueryCompilationContext(Dependencies, RelationalDependencies, async);
        }

    }

}
