using Apache.Calcite.EntityFrameworkCore.Query.Internal.Translators;

using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    public class CalciteMemberTranslatorProvider : RelationalMemberTranslatorProvider
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteMemberTranslatorProvider(RelationalMemberTranslatorProviderDependencies dependencies) :
            base(dependencies)
        {
            var sqlExpressionFactory = (CalciteSqlExpressionFactory)dependencies.SqlExpressionFactory;

            AddTranslators(
            [
                new CalciteDateTimeMemberTranslator(sqlExpressionFactory),
                new CalciteStringLengthTranslator(sqlExpressionFactory),
                new CalciteDateOnlyMemberTranslator(sqlExpressionFactory)
            ]);
        }

    }
}
