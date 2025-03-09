using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteParameterBasedSqlProcessorFactory : RelationalParameterBasedSqlProcessorFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteParameterBasedSqlProcessorFactory(RelationalParameterBasedSqlProcessorDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
