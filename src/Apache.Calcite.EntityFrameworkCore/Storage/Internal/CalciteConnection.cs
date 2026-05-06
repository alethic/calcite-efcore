using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

using Apache.Calcite.EntityFrameworkCore.Diagnostics.Internal;
using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteRelationalConnection : RelationalConnection, ICalciteConnection
    {

        static readonly CalciteTransaction FakeTransaction = new();

        /// <summary>
        /// Initializes the static instance.
        /// </summary>
        static CalciteRelationalConnection()
        {
            java.lang.Class.forName("org.apache.calcite.jdbc.Driver");
            java.lang.Class.forName("org.apache.calcite.server.ServerDdlExecutor");
        }

        readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;
        readonly IDiagnosticsLogger<DbLoggerCategory.Infrastructure> _logger;
        readonly int? _commandTimeout;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="rawSqlCommandBuilder"></param>
        /// <param name="logger"></param>
        public CalciteRelationalConnection(RelationalConnectionDependencies dependencies, IRawSqlCommandBuilder rawSqlCommandBuilder, IDiagnosticsLogger<DbLoggerCategory.Infrastructure> logger) :
            base(dependencies)
        {
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _logger = logger;

            var optionsExtension = dependencies.ContextOptions.Extensions.OfType<CalciteOptionsExtension>().FirstOrDefault();
            if (optionsExtension != null)
            {
                var relationalOptions = RelationalOptionsExtension.Extract(dependencies.ContextOptions);
                _commandTimeout = relationalOptions.CommandTimeout;

                if (relationalOptions.Connection != null)
                {
                    InitializeDbConnection(relationalOptions.Connection);
                }
            }
        }

        /// <inheritdoc/>
        protected override DbConnection CreateDbConnection()
        {
            var connection = new JdbcConnection(GetValidatedConnectionString());
            InitializeDbConnection(connection);
            return connection;
        }

        void InitializeDbConnection(DbConnection connection)
        {
            if (connection is JdbcConnection jdbcConnection)
            {
                if (_commandTimeout.HasValue)
                {

                }
            }
            else
            {
                _logger.UnexpectedConnectionTypeWarning(connection.GetType());
            }
        }

        /// <inheritdoc/>
        public override IDbContextTransaction? CurrentTransaction => null;

        /// <inheritdoc/>
        public override Transaction? EnlistedTransaction => null;

        /// <inheritdoc/>
        public override IDbContextTransaction BeginTransaction()
        {
            Dependencies.TransactionLogger.TransactionIgnoredWarning();
            return FakeTransaction;
        }

        /// <inheritdoc/>
        public override Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(BeginTransaction());
        }

        /// <inheritdoc/>
        public override void CommitTransaction()
        {
            Dependencies.TransactionLogger.TransactionIgnoredWarning();
        }

        /// <inheritdoc/>
        public override Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            CommitTransaction();
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override void RollbackTransaction()
        {
            Dependencies.TransactionLogger.TransactionIgnoredWarning();
        }

        /// <inheritdoc/>
        public override Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            RollbackTransaction();
            return Task.CompletedTask;
        }

    }

}
