using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Expressions.Internal
{

    /// <summary>
    /// The default query SQL generator for Calcite.
    /// </summary>
    public class CalciteQuerySqlGenerator : QuerySqlGenerator
    {

        /// <inheritdoc />
        public CalciteQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
