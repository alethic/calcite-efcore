using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryCompilationContext : RelationalQueryCompilationContext
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="async"></param>
        /// <param name="precompiling"></param>
#pragma warning disable EF9100 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public CalciteQueryCompilationContext(QueryCompilationContextDependencies dependencies, RelationalQueryCompilationContextDependencies relationalDependencies, bool async, bool precompiling) :
            base(dependencies, relationalDependencies, async, precompiling)
#pragma warning restore EF9100 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="async"></param>
        public CalciteQueryCompilationContext(QueryCompilationContextDependencies dependencies, RelationalQueryCompilationContextDependencies relationalDependencies, bool async)
            : this(dependencies, relationalDependencies, async, precompiling: false)
        {

        }

        /// <inheritdoc />
        public override bool SupportsPrecompiledQuery => true;

    }

}
