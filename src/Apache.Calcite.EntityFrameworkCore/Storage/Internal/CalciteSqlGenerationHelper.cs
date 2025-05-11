using System;
using System.Text;

using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

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
        public override string StatementTerminator => throw new NotSupportedException("Calcite does not support multiple statements per command.");

        /// <summary>
        /// Generates a valid parameter name for the given candidate name.
        /// </summary>
        /// <param name="name">The candidate name for the parameter.</param>
        /// <returns>
        /// A valid name based on the candidate name.
        /// </returns>
        public override string GenerateParameterName(string name)
            => name;

        /// <summary>
        /// Writes a valid parameter name for the given candidate name.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder" /> to write generated string to.</param>
        /// <param name="name">The candidate name for the parameter.</param>
        public override void GenerateParameterName(StringBuilder builder, string name)
            => builder.Append(name);

        /// <summary>
        /// Generates a valid parameter placeholder name for the given candidate name.
        /// </summary>
        /// <param name="name">The candidate name for the parameter placeholder.</param>
        /// <returns>
        /// A valid name based on the candidate name.
        /// </returns>
        public override string GenerateParameterNamePlaceholder(string name)
            => "?";

        /// <summary>
        /// Writes a valid parameter placeholder name for the given candidate name.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder" /> to write generated string to.</param>
        /// <param name="name">The candidate name for the parameter placeholder.</param>
        public override void GenerateParameterNamePlaceholder(StringBuilder builder, string name)
            => builder.Append('?');

    }

}
