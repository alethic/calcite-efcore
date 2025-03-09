using System.Collections.Generic;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteRegexMethodTranslator : IMethodCallTranslator
    {

        readonly CalciteSqlExpressionFactory sqlExpressionFactory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="sqlExpressionFactory"></param>
        public CalciteRegexMethodTranslator(CalciteSqlExpressionFactory sqlExpressionFactory)
        {
            this.sqlExpressionFactory = sqlExpressionFactory;
        }

        public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            return null;
        }

    }

}