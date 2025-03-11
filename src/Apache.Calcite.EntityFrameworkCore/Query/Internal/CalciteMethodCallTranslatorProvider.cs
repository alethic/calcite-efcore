using Apache.Calcite.EntityFrameworkCore.Query.Internal.Translators;

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

            AddTranslators(
            [
                new CalciteByteArrayMethodTranslator(sqlExpressionFactory),
                new CalciteCharMethodTranslator(sqlExpressionFactory),
                new CalciteDateOnlyMethodTranslator(sqlExpressionFactory),
                new CalciteDateTimeMethodTranslator(sqlExpressionFactory),
                new CalciteGlobMethodTranslator(sqlExpressionFactory),
                new CalciteHexMethodTranslator(sqlExpressionFactory),
                new CalciteMathTranslator(sqlExpressionFactory),
                new CalciteObjectToStringTranslator(sqlExpressionFactory),
                new CalciteRandomTranslator(sqlExpressionFactory),
                new CalciteRegexMethodTranslator(sqlExpressionFactory),
                new CalciteStringMethodTranslator(sqlExpressionFactory),
                new CalciteSubstrMethodTranslator(sqlExpressionFactory)
            ]);
        }

    }

}
