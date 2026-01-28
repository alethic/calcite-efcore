using Apache.Calcite.EntityFrameworkCore.Storage.Internal;

using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryCompilationContextFactory : IQueryCompilationContextFactory
    {

        readonly ICalciteRelationalConnection _calciteJdbcConnection;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="jetConnection"></param>
        public CalciteQueryCompilationContextFactory(QueryCompilationContextDependencies dependencies, RelationalQueryCompilationContextDependencies relationalDependencies, ICalciteRelationalConnection jetConnection)
        {
            _calciteJdbcConnection = jetConnection;
            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
        }

        /// <summary>
        /// Dependencies for this service.
        /// </summary>
        protected virtual QueryCompilationContextDependencies Dependencies { get; }

        /// <summary>
        /// Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalQueryCompilationContextDependencies RelationalDependencies { get; }

        public virtual QueryCompilationContext Create(bool async) => new CalciteQueryCompilationContext(Dependencies, RelationalDependencies, async);

        public virtual QueryCompilationContext CreatePrecompiled(bool async) => new CalciteQueryCompilationContext(Dependencies, RelationalDependencies, async, precompiling: true);

    }

}
