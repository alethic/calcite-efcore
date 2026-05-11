using System.Text;

using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    /// <inheritdoc/>
    public class CalciteSqlGenerationHelper : RelationalSqlGenerationHelper
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteSqlGenerationHelper(RelationalSqlGenerationHelperDependencies dependencies) :
            base(dependencies)
        {

        }

        /// <inheritdoc/>
        public override string StatementTerminator => "";

        /// <inheritdoc/>
        public override string GenerateParameterName(string name) => name;

        /// <inheritdoc/>
        public override void GenerateParameterName(StringBuilder builder, string name) => builder.Append(name);

        /// <inheritdoc/>
        public override string GenerateParameterNamePlaceholder(string name) => "?";

        /// <inheritdoc/>
        public override void GenerateParameterNamePlaceholder(StringBuilder builder, string name) => builder.Append('?');

    }

}
