using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteDatabaseCreator : RelationalDatabaseCreator
    {

        readonly ICalciteConnection _connection;
        readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="connection"></param>
        /// <param name="rawSqlCommandBuilder"></param>
        public CalciteDatabaseCreator(RelationalDatabaseCreatorDependencies dependencies, ICalciteConnection connection, IRawSqlCommandBuilder rawSqlCommandBuilder) :
            base(dependencies)
        {
            _connection = connection;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
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
            => Dependencies.ExecutionStrategy.Execute(
                _connection,
                connection => (int)CreateHasTablesCommand()
                        .ExecuteScalar(
                            new RelationalCommandParameterObject(
                                connection,
                                null,
                                null,
                                Dependencies.CurrentContext.Context,
                                Dependencies.CommandLogger, CommandSource.Migrations))!
                    != 0,
                null);

        /// <inheritdoc/>
        public override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default)
            => (long)(await Dependencies.ExecutionStrategy.ExecuteAsync(
                _connection,
                (connection, ct) => CreateHasTablesCommand()
                    .ExecuteScalarAsync(
                        new RelationalCommandParameterObject(
                            connection,
                            null,
                            null,
                            Dependencies.CurrentContext.Context,
                            Dependencies.CommandLogger, CommandSource.Migrations),
                        cancellationToken: ct),
                null,
                cancellationToken).ConfigureAwait(false))!
            > 0;

        IRelationalCommand CreateHasTablesCommand()
            => _rawSqlCommandBuilder
               .Build(@"
SELECT COUNT(*)
FROM ""metadata"".""TABLES""
");

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
        }

        /// <inheritdoc/>
        public override Task CreateTablesAsync(CancellationToken cancellationToken = default)
        {
            return base.CreateTablesAsync(cancellationToken);
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
