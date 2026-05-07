using System.Collections.Generic;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    /// <summary>
    /// Translates <see cref="string"/> instance methods into Calcite SQL expressions.
    /// </summary>
    public class CalciteStringMethodTranslator : IMethodCallTranslator
    {

        static readonly MethodInfo ContainsMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Contains), [typeof(string)])!;

        readonly CalciteSqlExpressionFactory _sqlExpressionFactory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="sqlExpressionFactory"></param>
        public CalciteStringMethodTranslator(CalciteSqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        /// <inheritdoc/>
        public virtual SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (instance is null)
                return null;

            if (!Equals(method, ContainsMethodInfo))
                return null;

            var search = arguments[0];
            var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, search);
            instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
            search = _sqlExpressionFactory.ApplyTypeMapping(search, stringTypeMapping);

            // instance LIKE ('%' || search || '%')
            var percent = _sqlExpressionFactory.Constant("%", stringTypeMapping);
            var pattern = _sqlExpressionFactory.Add(
                _sqlExpressionFactory.Add(percent, search),
                percent);

            return _sqlExpressionFactory.Like(instance, pattern);
        }

    }

}
