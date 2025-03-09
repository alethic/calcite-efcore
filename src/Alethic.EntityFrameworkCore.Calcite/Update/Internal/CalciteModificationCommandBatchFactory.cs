using System;
using System.Linq;

using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Update;

namespace Alethic.EntityFrameworkCore.Calcite.Update.Internal
{

    class CalciteModificationCommandBatchFactory : IModificationCommandBatchFactory
    {

        const int DefaultMaxBatchSize = 42;
        const int MaxMaxBatchSize = 1000;

        readonly int _maxBatchSize;

        public CalciteModificationCommandBatchFactory(
            ModificationCommandBatchFactoryDependencies dependencies,
            IDbContextOptions options)
        {
            Dependencies = dependencies;

            _maxBatchSize = Math.Min(
                options.Extensions.OfType<CalciteOptionsExtension>().FirstOrDefault()?.MaxBatchSize ?? DefaultMaxBatchSize,
                MaxMaxBatchSize);

            if (_maxBatchSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(RelationalOptionsExtension.MaxBatchSize), RelationalStrings.InvalidMaxBatchSize(_maxBatchSize));
        }

        protected virtual ModificationCommandBatchFactoryDependencies Dependencies { get; }

        public ModificationCommandBatch Create()
        {
            throw new NotImplementedException();
        }

    }

}
