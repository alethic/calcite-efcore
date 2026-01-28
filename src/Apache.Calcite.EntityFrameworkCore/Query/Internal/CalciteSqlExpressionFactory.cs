using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteSqlExpressionFactory : SqlExpressionFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteSqlExpressionFactory(SqlExpressionFactoryDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
