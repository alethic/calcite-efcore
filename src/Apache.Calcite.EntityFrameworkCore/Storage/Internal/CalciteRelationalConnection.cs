using System.Data.Common;
using System.Linq;

using Apache.Calcite.EntityFrameworkCore.Extensions.Internal;
using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteRelationalConnection : RelationalConnection, ICalciteRelationalConnection
    {

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

    }

}
