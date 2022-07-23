using System.Reflection;

using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions
{

    /// <summary>
    /// Calcite specific extension methods for <see cref="DbContext.Database" />.
    /// </summary>
    public static class CalciteDatabaseFacadeExtensions
    {


        /// <summary>
        /// <para>
        /// Returns true if the database provider currently in use is the Calcite provider.
        /// </para>
        /// <para>
        /// This method can only be used after the <see cref="DbContext" /> has been configured because
        /// it is only then that the provider is known. This means that this method cannot be used
        /// in <see cref="DbContext.OnConfiguring" /> because this is where application code sets the
        /// provider to use as part of configuring the context.
        /// </para>
        /// </summary>
        /// <param name="database">The facade from <see cref="DbContext.Database" />.</param>
        /// <returns>True if Calcite is being used; false otherwise.</returns>
        public static bool IsNpgsql(this DatabaseFacade database)
            => database.ProviderName == typeof(CalciteOptionsExtension).GetTypeInfo().Assembly.GetName().Name;

    }

}
