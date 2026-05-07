using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    /// <summary>
    /// Calcite-specific extension of <see cref="IValueGeneratorCache"/> that also caches
    /// <see cref="CalciteEntitySequenceGeneratorState"/> instances keyed by entity sequence and connection.
    /// </summary>
    public interface ICalciteValueGeneratorCache : IValueGeneratorCache
    {

        /// <summary>
        /// Gets the cached <see cref="CalciteEntitySequenceGeneratorState"/> for the entity sequence associated with
        /// the specified <paramref name="property"/> and <paramref name="connection"/>, creating and caching a new
        /// instance if one does not already exist.
        /// </summary>
        /// <param name="property">The property whose entity sequence state is being retrieved.</param>
        /// <param name="connection">The relational connection used to scope the cached state.</param>
        /// <returns>The cached or newly created <see cref="CalciteEntitySequenceGeneratorState"/>.</returns>
        CalciteEntitySequenceGeneratorState GetOrAddEntitySequenceState(IReadOnlyProperty property, IRelationalConnection connection);

    }

}
