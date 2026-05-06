using System;
using System.Collections.Concurrent;

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Metadata;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    public class CalciteValueGeneratorCache : ValueGeneratorCache, ICalciteValueGeneratorCache
    {

        readonly ConcurrentDictionary<string, CalciteEntitySequenceGeneratorState> _cache = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteValueGeneratorCache(ValueGeneratorCacheDependencies dependencies) :
            base(dependencies)
        {

        }

        public virtual CalciteEntitySequenceGeneratorState GetOrAddEntitySequenceState(IReadOnlyProperty property, IRelationalConnection connection)
        {
            var tableIdentifier = StoreObjectIdentifier.Create(property.DeclaringType, StoreObjectType.Table);
            var entitySequence = property.FindEntitySequence();
            if (entitySequence is null)
                throw new InvalidOperationException("Entity sequence name cannot be null.");

            return _cache.GetOrAdd(GetEntitySequenceKey(entitySequence, connection), _ => new CalciteEntitySequenceGeneratorState(entitySequence));
        }

        /// <summary>
        /// Gets a unique cache key name for the given sequence and connection.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        static string GetEntitySequenceKey(ICalciteReadOnlyEntitySequence sequence, IRelationalConnection connection)
        {
            var jdbcConnection = (JdbcConnection)connection.DbConnection;
            return $"{jdbcConnection.ConnectionString}::{sequence.Name}";
        }

    }

}
