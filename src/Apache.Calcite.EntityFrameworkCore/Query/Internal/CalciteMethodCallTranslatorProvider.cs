using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteMethodCallTranslatorProvider : RelationalMethodCallTranslatorProvider
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteMethodCallTranslatorProvider(RelationalMethodCallTranslatorProviderDependencies dependencies) :
            base(dependencies)
        {
            var sqlExpressionFactory = (CalciteSqlExpressionFactory)dependencies.SqlExpressionFactory;
            AddTranslators([]);
        }

    }

}
