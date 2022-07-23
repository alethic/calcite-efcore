using System;
using System.Data.Common;

using Alethic.EntityFrameworkCore.Calcite.Infrastructure;
using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions
{

    /// <summary>
    /// Provides extension methods on <see cref="DbContextOptionsBuilder"/> and <see cref="DbContextOptionsBuilder{T}"/>
    /// used to configure a <see cref="DbContext"/> to context to a PostgreSQL database with Apache Calcite.
    /// </summary>
    public static class CalciteDbContextOptionsBuilderExtensions
    {


        /// <summary>
        /// <para>
        /// Configures the context to connect to Apache Calcite, but without initially setting any <see
        /// cref="DbConnection" /> or connection string.
        /// </para>
        /// <para>
        /// The connection or connection string must be set before the <see cref="DbContext" /> is used to connect
        /// to a database. Set a connection using <see cref="RelationalDatabaseFacadeExtensions.SetDbConnection" />.
        /// Set a connection string using <see cref="RelationalDatabaseFacadeExtensions.SetConnectionString" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional Calcite-specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder UseCalcite(this DbContextOptionsBuilder optionsBuilder, Action<CalciteDbContextOptionsBuilder> calciteOptionsAction = null)
        {
            if (optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(GetOrCreateExtension(optionsBuilder));

            ConfigureWarnings(optionsBuilder);

            calciteOptionsAction?.Invoke(new CalciteDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Configures the context to connect to Apache Calcite.
        /// </summary>
        /// <param name="optionsBuilder">A builder for setting options on the context.</param>
        /// <param name="connection">
        /// An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        /// in the open state then EF will not open or close the connection. If the connection is in the closed
        /// state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="calciteOptionsAction">An optional action to allow additional Npgsql-specific configuration.</param>
        /// <returns>
        /// The options builder so that further configuration can be chained.
        /// </returns>
        public static DbContextOptionsBuilder UseCalcite(
            this DbContextOptionsBuilder optionsBuilder,
            DbConnection connection,
            Action<CalciteDbContextOptionsBuilder> calciteOptionsAction = null)
        {
            if (optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            var extension = (CalciteOptionsExtension)GetOrCreateExtension(optionsBuilder).WithConnection(connection);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            calciteOptionsAction?.Invoke(new CalciteDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// <para>
        /// Configures the context to connect to Apache Calcite, but without initially setting any
        /// <see cref="DbConnection" /> or connection string.
        /// </para>
        /// <para>
        /// The connection or connection string must be set before the <see cref="DbContext" /> is used to connect
        /// to a database. Set a connection using <see cref="RelationalDatabaseFacadeExtensions.SetDbConnection" />.
        /// Set a connection string using <see cref="RelationalDatabaseFacadeExtensions.SetConnectionString" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional Npgsql-specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder<TContext> UseCalcite<TContext>(this DbContextOptionsBuilder<TContext> optionsBuilder, Action<CalciteDbContextOptionsBuilder> calciteOptionsAction = null)
            where TContext : DbContext
        {
            return (DbContextOptionsBuilder<TContext>)UseCalcite((DbContextOptionsBuilder)optionsBuilder, calciteOptionsAction);
        }

        /// <summary>
        /// Returns an existing instance of <see cref="CalciteOptionsExtension"/>, or a new instance if one does not exist.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/> to search.</param>
        /// <returns>
        /// An existing instance of <see cref="CalciteOptionsExtension"/>, or a new instance if one does not exist.
        /// </returns>
        static CalciteOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<CalciteOptionsExtension>() is { } existing
                ? new CalciteOptionsExtension(existing)
                : new CalciteOptionsExtension();


        static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsExtension = optionsBuilder.Options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension();
            coreOptionsExtension = coreOptionsExtension.WithWarningsConfiguration(coreOptionsExtension.WarningsConfiguration.TryWithExplicit(RelationalEventId.AmbientTransactionWarning, WarningBehavior.Throw));
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        }

    }

}
