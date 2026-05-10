using System;
using System.Collections.Concurrent;

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Metadata;

using Apache.Calcite.Data;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    /// <summary>
    /// Caches <see cref="CalciteEntitySequenceGeneratorState"/> instances keyed by entity sequence and connection,
    /// extending the standard EF Core <see cref="ValueGeneratorCache"/> with Calcite-specific sequence support.
    /// </summary>
    public class CalciteValueGeneratorCache : ValueGeneratorCache, ICalciteValueGeneratorCache
    {

        readonly ConcurrentDictionary<string, CalciteEntitySequenceGeneratorState> _cache = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CalciteValueGeneratorCache"/> class.
        /// </summary>
        /// <param name="dependencies">The dependencies required by the base <see cref="ValueGeneratorCache"/>.</param>
        public CalciteValueGeneratorCache(ValueGeneratorCacheDependencies dependencies) :
            base(dependencies)
        {

        }

        /// <summary>
        /// Gets the cached <see cref="CalciteEntitySequenceGeneratorState"/> for the entity sequence associated with
        /// the specified <paramref name="property"/> and <paramref name="connection"/>, creating and caching a new
        /// instance if one does not already exist.
        /// </summary>
        /// <param name="property">The property whose entity sequence state is being retrieved.</param>
        /// <param name="connection">The relational connection used to scope the cached state.</param>
        /// <returns>The cached or newly created <see cref="CalciteEntitySequenceGeneratorState"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no entity sequence is configured for the property.</exception>
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
            var calciteConnection = (CalciteConnection)connection.DbConnection;
            return $"{calciteConnection.ConnectionString}::{sequence.Name}";
        }

    }

}
