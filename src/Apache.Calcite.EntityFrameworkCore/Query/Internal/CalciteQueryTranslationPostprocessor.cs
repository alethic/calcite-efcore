using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteQueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {


        readonly IRelationalTypeMappingSource _relationalTypeMappingSource;
        readonly ICalciteOptions _options;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="queryCompilationContext"></param>
        /// <param name="relationalTypeMappingSource"></param>
        /// <param name="options"></param>
        public CalciteQueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies, RelationalQueryTranslationPostprocessorDependencies relationalDependencies, RelationalQueryCompilationContext queryCompilationContext, IRelationalTypeMappingSource relationalTypeMappingSource, ICalciteOptions options) :
            base(dependencies, relationalDependencies, queryCompilationContext)
        {
            _relationalTypeMappingSource = relationalTypeMappingSource;
            _options = options;
        }

    }

}
