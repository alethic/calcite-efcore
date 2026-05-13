using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Apache.Calcite.Data;
using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using java.lang;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteDatabaseCreator : RelationalDatabaseCreator
    {

        readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;
        readonly ISqlGenerationHelper _sqlGenerationHelper;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="rawSqlCommandBuilder"></param>
        /// <param name="sqlGenerationHelper"></param>
        public CalciteDatabaseCreator(RelationalDatabaseCreatorDependencies dependencies, IRawSqlCommandBuilder rawSqlCommandBuilder, ISqlGenerationHelper sqlGenerationHelper) :
            base(dependencies)
        {
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _sqlGenerationHelper = sqlGenerationHelper;
        }

        /// <inheritdoc/>
        public override bool Exists()
        {
            return true;
        }

        /// <inheritdoc/>
        public override Task<bool> ExistsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public override bool HasTables()
        {
            return Dependencies.ExecutionStrategy.Execute(
                Dependencies.Connection,
                connection =>
                {
                    connection.Open();

                    try
                    {
                        return HasUserTables((CalciteConnection)connection.DbConnection);
                    }
                    finally
                    {
                        connection.Close();
                    }
                },
                null);
        }

        /// <inheritdoc/>
        public override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default)
        {
            return await Dependencies.ExecutionStrategy.ExecuteAsync(
                Dependencies.Connection,
                async (connection, ct) =>
                {
                    await connection.OpenAsync(ct).ConfigureAwait(false);

                    try
                    {
                        return HasUserTables((CalciteConnection)connection.DbConnection);
                    }
                    finally
                    {
                        await connection.CloseAsync().ConfigureAwait(false);
                    }
                },
                null,
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns <c>true</c>
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        static bool HasUserTables(CalciteConnection connection)
        {
            // check root-level tables
            if (connection.RootSchema.getTableNames().size() > 0)
                return true;

            // check other schemas besides metadata
            foreach (var schemaName in connection.RootSchema.getSubSchemaNames().AsEnumerable<string>())
            {
                if (schemaName == "metadata")
                    continue;

                var schema = connection.RootSchema.getSubSchema(schemaName);
                if (schema is null)
                    continue;

                if (schema.getTableNames().size() > 0)
                    return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override void Create()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task CreateAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void CreateTables()
        {
            base.CreateTables();
            SeedEntitySequenceRows();
        }

        /// <inheritdoc/>
        public override async Task CreateTablesAsync(CancellationToken cancellationToken = default)
        {
            await base.CreateTablesAsync(cancellationToken).ConfigureAwait(false);
            await SeedEntitySequenceRowsAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// After table creation, insert one row per registered <see cref="ICalciteEntitySequence"/> into its
        /// backing entity table so the HiLo value generator's UPDATE/SELECT against that row succeeds.
        /// </summary>
        void SeedEntitySequenceRows()
        {
            var commands = BuildSeedCommands();
            if (commands.Count == 0)
                return;

            Dependencies.Connection.Open();
            try
            {
                foreach (var sql in commands)
                    ExecuteSeed(sql);
            }
            finally
            {
                Dependencies.Connection.Close();
            }
        }

        async Task SeedEntitySequenceRowsAsync(CancellationToken cancellationToken)
        {
            var commands = BuildSeedCommands();
            if (commands.Count == 0)
                return;

            await Dependencies.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                foreach (var sql in commands)
                    await ExecuteSeedAsync(sql, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                await Dependencies.Connection.CloseAsync().ConfigureAwait(false);
            }
        }

        void ExecuteSeed(string sql)
        {
            _rawSqlCommandBuilder.Build(sql).ExecuteNonQuery(
                new RelationalCommandParameterObject(
                    Dependencies.Connection,
                    null,
                    null,
                    Dependencies.CurrentContext.Context,
                    Dependencies.CommandLogger,
                    CommandSource.Migrations));
        }

        Task ExecuteSeedAsync(string sql, CancellationToken cancellationToken)
        {
            return _rawSqlCommandBuilder.Build(sql).ExecuteNonQueryAsync(
                new RelationalCommandParameterObject(
                    Dependencies.Connection,
                    null,
                    null,
                    Dependencies.CurrentContext.Context,
                    Dependencies.CommandLogger,
                    CommandSource.Migrations),
                cancellationToken);
        }

        List<string> BuildSeedCommands()
        {
            var model = Dependencies.CurrentContext.Context.Model;
            var sequences = CalciteEntitySequence.GetEntitySequences(model);
            var commands = new List<string>();

            foreach (var sequence in sequences)
            {
                if (sequence.KeyValue is null)
                    continue;

                var sql = BuildSeedSql(sequence);
                if (sql != null)
                    commands.Add(sql);
            }

            return commands;
        }

        /// <summary>
        /// Builds an INSERT statement that seeds the single row identified by <see cref="ICalciteEntitySequence.KeyValue"/>
        /// in the backing entity table. The value column is initialized to the configured start value (1 by default).
        /// </summary>
        string? BuildSeedSql(ICalciteEntitySequence sequence)
        {
            var entityType = sequence.EntityType;
            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null || primaryKey.Properties.Count != 1)
                return null;

            var keyProperty = primaryKey.Properties[0];
            var valueProperty = sequence.ValueProperty;
            if (valueProperty == null)
                return null;

            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            if (string.IsNullOrEmpty(tableName))
                return null;

            var qualifiedTable = _sqlGenerationHelper.DelimitIdentifier(tableName, schema);
            var keyColumn = _sqlGenerationHelper.DelimitIdentifier(keyProperty.GetColumnName());
            var valueColumn = _sqlGenerationHelper.DelimitIdentifier(valueProperty.GetColumnName());

            var keyLiteral = FormatLiteral(sequence.KeyValue!);
            var valueLiteral = FormatLiteral(Convert.ChangeType(CalciteEntitySequence.DefaultStartValue, valueProperty.ClrType));

            var sb = new System.Text.StringBuilder();
            sb.Append("INSERT INTO ").Append(qualifiedTable)
                .Append(" (").Append(keyColumn).Append(", ").Append(valueColumn).Append(") ")
                .Append("VALUES (").Append(keyLiteral).Append(", ").Append(valueLiteral).Append(')');

            return sb.ToString();
        }

        static string FormatLiteral(object value)
        {
            return value switch
            {
                string s => "'" + s.Replace("'", "''") + "'",
                bool b => b ? "TRUE" : "FALSE",
                IFormattable f => f.ToString(null, System.Globalization.CultureInfo.InvariantCulture),
                _ => value.ToString() ?? "NULL"
            };
        }

        /// <inheritdoc/>
        public override void Delete()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }

}
