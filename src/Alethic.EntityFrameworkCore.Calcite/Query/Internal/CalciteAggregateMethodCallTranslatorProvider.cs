﻿using Alethic.EntityFrameworkCore.Calcite.Query.Internal.Translators;

using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    class CalciteAggregateMethodCallTranslatorProvider : RelationalAggregateMethodCallTranslatorProvider
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteAggregateMethodCallTranslatorProvider(RelationalAggregateMethodCallTranslatorProviderDependencies dependencies) :
            base(dependencies)
        {
            var sqlExpressionFactory = (CalciteSqlExpressionFactory)dependencies.SqlExpressionFactory;

            AddTranslators(
            [
                new CalciteQueryableAggregateMethodTranslator(sqlExpressionFactory),
                new CalciteStringAggregateMethodTranslator(sqlExpressionFactory)
            ]);
        }

    }

}
