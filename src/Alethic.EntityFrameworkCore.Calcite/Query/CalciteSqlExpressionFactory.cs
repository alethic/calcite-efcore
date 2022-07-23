using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query
{

    /// <inheritdoc />
    public class CalciteSqlExpressionFactory : SqlExpressionFactory
    {

        /// <inheritdoc />
        public CalciteSqlExpressionFactory(SqlExpressionFactoryDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
