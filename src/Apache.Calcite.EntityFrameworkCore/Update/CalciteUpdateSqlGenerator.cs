using System;
using System.Text;

using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.Update
{

    /// <inheritdoc/>
    public class CalciteUpdateSqlGenerator : UpdateAndSelectSqlGenerator
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteUpdateSqlGenerator(UpdateSqlGeneratorDependencies dependencies) :
            base(dependencies)
        {

        }

        /// <inheritdoc/>
        protected override void AppendIdentityWhereCondition(StringBuilder commandStringBuilder, IColumnModification columnModification)
        {
            if (commandStringBuilder is null)
                throw new ArgumentNullException(nameof(commandStringBuilder));
            if (columnModification is null)
                throw new ArgumentNullException(nameof(columnModification));

            SqlGenerationHelper.DelimitIdentifier(commandStringBuilder, "rowid");
            commandStringBuilder.Append(" = ").Append("last_insert_rowid()");
        }

        /// <inheritdoc/>
        protected override void AppendRowsAffectedWhereCondition(StringBuilder commandStringBuilder, int expectedRowsAffected)
        {
            if (commandStringBuilder is null)
                throw new ArgumentNullException(nameof(commandStringBuilder));

            commandStringBuilder.Append("changes() = ").Append(expectedRowsAffected);
        }

        /// <inheritdoc/>
        protected override ResultSetMapping AppendSelectAffectedCountCommand(StringBuilder commandStringBuilder, string name, string? schema, int commandPosition)
        {
            if (commandStringBuilder is null)
                throw new ArgumentNullException(nameof(commandStringBuilder));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            commandStringBuilder
                .Append("SELECT changes()")
                .AppendLine(SqlGenerationHelper.StatementTerminator)
                .AppendLine();

            return ResultSetMapping.LastInResultSet | ResultSetMapping.ResultSetWithRowsAffectedOnly;
        }

    }

}
