using System;
using System.Linq;

using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.Update.Internal
{

    class CalciteModificationCommandBatchFactory : IModificationCommandBatchFactory
    {

        const int DefaultMaxBatchSize = 42;
        const int MaxMaxBatchSize = 1000;

        readonly int _maxBatchSize;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CalciteModificationCommandBatchFactory(ModificationCommandBatchFactoryDependencies dependencies, IDbContextOptions options)
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
            return new CalciteModificationCommandBatch(Dependencies, _maxBatchSize);
        }

    }

}
