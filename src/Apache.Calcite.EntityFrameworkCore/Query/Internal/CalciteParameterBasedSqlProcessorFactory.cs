using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteParameterBasedSqlProcessorFactory : IRelationalParameterBasedSqlProcessorFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteParameterBasedSqlProcessorFactory(RelationalParameterBasedSqlProcessorDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        /// <summary>
        /// Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalParameterBasedSqlProcessorDependencies Dependencies { get; }

        /// <inheritdoc/>
        public RelationalParameterBasedSqlProcessor Create(RelationalParameterBasedSqlProcessorParameters parameters)
        {
            return new CalciteParameterBasedSqlProcessor(Dependencies, parameters);
        }

    }

}
