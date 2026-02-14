using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteDatabaseCreator : RelationalDatabaseCreator
    {

        readonly ICalciteRelationalConnection _connection;
        readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="connection"></param>
        /// <param name="rawSqlCommandBuilder"></param>
        public CalciteDatabaseCreator(RelationalDatabaseCreatorDependencies dependencies, ICalciteRelationalConnection connection, IRawSqlCommandBuilder rawSqlCommandBuilder) :
            base(dependencies)
        {
            _connection = connection;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override Task CreateAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool HasTables()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override bool Exists()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> ExistsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }

}
