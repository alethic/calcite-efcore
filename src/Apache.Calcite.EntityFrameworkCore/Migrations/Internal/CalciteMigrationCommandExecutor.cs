using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Migrations.Internal
{

    public class CalciteMigrationCommandExecutor : MigrationCommandExecutor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="executionStrategy"></param>
        public CalciteMigrationCommandExecutor(IExecutionStrategy executionStrategy) :
            base(executionStrategy)
        {

        }

        /// <inheritdoc/>
        public override void ExecuteNonQuery(IEnumerable<MigrationCommand> migrationCommands, IRelationalConnection connection)
        {
            base.ExecuteNonQuery(migrationCommands, connection);
        }

        /// <inheritdoc/>
        public override int ExecuteNonQuery(IReadOnlyList<MigrationCommand> migrationCommands, IRelationalConnection connection, MigrationExecutionState executionState, bool commitTransaction, IsolationLevel? isolationLevel = null)
        {
            return base.ExecuteNonQuery(migrationCommands, connection, executionState, commitTransaction, isolationLevel);
        }

        /// <inheritdoc/>
        public override Task ExecuteNonQueryAsync(IEnumerable<MigrationCommand> migrationCommands, IRelationalConnection connection, CancellationToken cancellationToken = default)
        {
            return base.ExecuteNonQueryAsync(migrationCommands, connection, cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<int> ExecuteNonQueryAsync(IReadOnlyList<MigrationCommand> migrationCommands, IRelationalConnection connection, MigrationExecutionState executionState, bool commitTransaction, IsolationLevel? isolationLevel = null, CancellationToken cancellationToken = default)
        {
            return base.ExecuteNonQueryAsync(migrationCommands, connection, executionState, commitTransaction, isolationLevel, cancellationToken);
        }

    }

}
