using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.Update.Internal
{

    /// <inheritdoc/>
    public class CalciteUpdateSqlGenerator : UpdateSqlGenerator
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
        protected override void AppendInsertCommand(StringBuilder commandStringBuilder, string name, string? schema, IReadOnlyList<IColumnModification> writeOperations, IReadOnlyList<IColumnModification> readOperations)
        {
            AppendInsertCommandHeader(commandStringBuilder, name, schema, writeOperations);
            AppendValuesHeader(commandStringBuilder, writeOperations);
            AppendValues(commandStringBuilder, name, schema, writeOperations);
            SetGetGeneratedKeys(commandStringBuilder, readOperations);
        }

        /// <inheritdoc/>
        public override ResultSetMapping AppendUpdateOperation(StringBuilder commandStringBuilder, IReadOnlyModificationCommand command, int commandPosition, out bool requiresTransaction)
        {
            var writeOperations = command.ColumnModifications.Where(o => o.IsWrite).ToList();
            var conditionOperations = command.ColumnModifications.Where(o => o.IsCondition).ToList();
            var readOperations = command.ColumnModifications.Where(o => o.IsRead).ToList();

            requiresTransaction = false;

            AppendUpdateCommand(commandStringBuilder, command.TableName, command.Schema, writeOperations, readOperations, conditionOperations);

            return readOperations.Count > 0
                ? ResultSetMapping.LastInResultSet
                : ResultSetMapping.LastInResultSet | ResultSetMapping.NoResults;
        }

        /// <inheritdoc/>
        protected override void AppendUpdateCommand(StringBuilder commandStringBuilder, string name, string? schema, IReadOnlyList<IColumnModification> writeOperations, IReadOnlyList<IColumnModification> readOperations, IReadOnlyList<IColumnModification> conditionOperations, bool appendReturningOneClause = false)
        {
            if (appendReturningOneClause)
                throw new InvalidOperationException();

            AppendUpdateCommandHeader(commandStringBuilder, name, schema, writeOperations);
            AppendWhereClause(commandStringBuilder, conditionOperations);
            SetGetGeneratedKeys(commandStringBuilder, readOperations);
        }

        /// <inheritdoc/>
        protected void SetGetGeneratedKeys(StringBuilder commandStringBuilder, IReadOnlyList<IColumnModification> operations)
        {
            if (operations.Count > 0)
            {
                commandStringBuilder
                    .AppendLine("-- :GetGeneratedKeys");
            }
        }

    }

}
