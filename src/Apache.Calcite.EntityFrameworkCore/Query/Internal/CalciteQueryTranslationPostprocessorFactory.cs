using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryTranslationPostprocessorFactory : IQueryTranslationPostprocessorFactory
    {

        readonly QueryTranslationPostprocessorDependencies _dependencies;
        readonly RelationalQueryTranslationPostprocessorDependencies _relationalDependencies;
        readonly IRelationalTypeMappingSource _relationalTypeMappingSource;
        readonly ICalciteOptions _options;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="relationalTypeMappingSource"></param>
        /// <param name="options"></param>
        public CalciteQueryTranslationPostprocessorFactory(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies, IRelationalTypeMappingSource relationalTypeMappingSource, ICalciteOptions options)
        {
            _dependencies = dependencies;
            _relationalDependencies = relationalDependencies;
            _relationalTypeMappingSource = relationalTypeMappingSource;
            _options = options;
        }

        public virtual QueryTranslationPostprocessor Create(QueryCompilationContext queryCompilationContext)
            => new CalciteQueryTranslationPostprocessor(
                _dependencies,
                _relationalDependencies,
                (RelationalQueryCompilationContext)queryCompilationContext,
                _relationalTypeMappingSource,
                _options);

    }

}
