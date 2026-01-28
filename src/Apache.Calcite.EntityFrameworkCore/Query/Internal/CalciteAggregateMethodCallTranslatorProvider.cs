using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteAggregateMethodCallTranslatorProvider : RelationalAggregateMethodCallTranslatorProvider
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteAggregateMethodCallTranslatorProvider(RelationalAggregateMethodCallTranslatorProviderDependencies dependencies) :
            base(dependencies)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;
            var typeMappingSource = dependencies.RelationalTypeMappingSource;
            AddTranslators([]);
        }

    }

}
