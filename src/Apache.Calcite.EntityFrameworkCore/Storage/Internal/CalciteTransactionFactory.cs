using System;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteTransactionFactory : IRelationalTransactionFactory
    {

        readonly RelationalTransactionFactoryDependencies _dependencies;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteTransactionFactory(RelationalTransactionFactoryDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        /// <summary>
        /// Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalTransactionFactoryDependencies Dependencies => _dependencies;

        public virtual RelationalTransaction Create(IRelationalConnection connection, DbTransaction transaction, Guid transactionId, IDiagnosticsLogger<DbLoggerCategory.Database.Transaction> logger, bool transactionOwned)
            => new CalciteTransaction(connection, transaction, transactionId, logger, transactionOwned, Dependencies.SqlGenerationHelper);

    }

}
