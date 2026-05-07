using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal
{

    /// <summary>
    /// Singleton options contract for the Calcite Entity Framework Core provider.
    /// </summary>
    public interface ICalciteOptions : ISingletonOptions
    {

        /// <summary>
        /// Gets the connection string used by the Calcite provider, or <see langword="null"/> if a connection instance was supplied directly.
        /// </summary>
        string? ConnectionString { get; }

    }

}
