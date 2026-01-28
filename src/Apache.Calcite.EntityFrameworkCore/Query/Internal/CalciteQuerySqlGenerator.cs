using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQuerySqlGenerator : QuerySqlGenerator, ICalciteExpressionVisitor
    {

        readonly ITypeMappingSource _typeMappingSource;
        readonly ICalciteOptions _options;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="typeMappingSource"></param>
        /// <param name="options"></param>
        public CalciteQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies, ITypeMappingSource typeMappingSource, ICalciteOptions options) :
            base(dependencies)
        {
            _typeMappingSource = typeMappingSource;
            _options = options;
        }

    }

}
