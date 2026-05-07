using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    /// <summary>
    /// Marker interface representing the Calcite-specific <see cref="IRelationalConnection"/> used by the provider.
    /// </summary>
    public interface ICalciteConnection : IRelationalConnection
    {



    }

}
