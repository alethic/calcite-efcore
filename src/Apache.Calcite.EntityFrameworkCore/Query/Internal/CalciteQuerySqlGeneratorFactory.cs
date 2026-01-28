using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQuerySqlGeneratorFactory : IQuerySqlGeneratorFactory
    {

        readonly QuerySqlGeneratorDependencies _dependencies;
        readonly ITypeMappingSource _typeMappingSource;
        readonly ICalciteOptions _options;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="typeMappingSource"></param>
        /// <param name="options"></param>
        public CalciteQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies, ITypeMappingSource typeMappingSource, ICalciteOptions options)
        {
            _dependencies = dependencies;
            _typeMappingSource = typeMappingSource;
            _options = options;
        }

        public virtual QuerySqlGenerator Create()
            => new CalciteQuerySqlGenerator(_dependencies, _typeMappingSource, _options);

    }

}
