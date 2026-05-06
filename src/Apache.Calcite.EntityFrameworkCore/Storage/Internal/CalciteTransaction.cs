using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteTransaction : IDbContextTransaction
    {

        /// <inheritdoc/>
        public virtual Guid TransactionId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public virtual void Commit()
        {

        }

        /// <inheritdoc/>
        public virtual Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual void Rollback()
        {

        }

        /// <inheritdoc/>
        public virtual Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {

        }

        /// <inheritdoc/>
        public virtual ValueTask DisposeAsync()
        {
            return default;
        }

    }

}
