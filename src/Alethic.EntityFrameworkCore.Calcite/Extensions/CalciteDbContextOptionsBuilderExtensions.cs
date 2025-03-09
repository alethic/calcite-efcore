using System;
using System.Data.Common;

using Alethic.EntityFrameworkCore.Calcite.Infrastructure;
using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions
{

    /// <summary>
    /// Provides extension methods on <see cref="DbContextOptionsBuilder"/> and <see cref="DbContextOptionsBuilder{T}"/>
    /// used to configure a <see cref="DbContext"/> to context to Apache Calcite.
    /// </summary>
    public static class CalciteDbContextOptionsBuilderExtensions
    {

        /// <summary>
        /// Configures the context to connect to a Calcite database, but without initially setting any
        /// <see cref="DbConnection" /> or connection string.
        /// </summary>
        /// <remarks>
        /// The connection or connection string must be set before the <see cref="DbContext" /> is used to connect
        /// to a database. Set a connection using <see cref="RelationalDatabaseFacadeExtensions.SetDbConnection" />.
        /// Set a connection string using <see cref="RelationalDatabaseFacadeExtensions.SetConnectionString" />.
        /// </remarks>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder UseCalcite(this DbContextOptionsBuilder optionsBuilder, Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null)
        {
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(GetOrCreateExtension(optionsBuilder));

            ConfigureWarnings(optionsBuilder);

            calciteOptionsAction?.Invoke(new CalciteDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Configures the context to connect to a Calcite database.
        /// </summary>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="connectionString">The connection string of the database to connect to.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional Calcite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder UseCalcite(
            this DbContextOptionsBuilder optionsBuilder,
            string? connectionString,
            Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null)
        {
            var extension = (CalciteOptionsExtension)GetOrCreateExtension(optionsBuilder).WithConnectionString(connectionString);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            calciteOptionsAction?.Invoke(new CalciteDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Configures the context to connect to a Calcite database.
        /// </summary>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="connection">
        /// An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        /// in the open state then EF will not open or close the connection. If the connection is in the closed
        /// state then EF will open and close the connection as needed. The caller owns the connection and is
        /// responsible for its disposal.
        /// </param>
        /// <param name="sqliteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder UseSqlite(
            this DbContextOptionsBuilder optionsBuilder,
            DbConnection connection,
            Action<CalciteDbContextOptionsBuilder>? sqliteOptionsAction = null)
            => UseCalcite(optionsBuilder, connection, false, sqliteOptionsAction);

        /// <summary>
        /// Configures the context to connect to a Calcite database.
        /// </summary>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="connection">
        ///     An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        ///     in the open state then EF will not open or close the connection. If the connection is in the closed
        ///     state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="contextOwnsConnection">
        ///     If <see langword="true" />, then EF will take ownership of the connection and will
        ///     dispose it in the same way it would dispose a connection created by EF. If <see langword="false" />, then the caller still
        ///     owns the connection and is responsible for its disposal.
        /// </param>
        /// <param name="sqliteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder UseCalcite(
            this DbContextOptionsBuilder optionsBuilder,
            DbConnection connection,
            bool contextOwnsConnection,
            Action<CalciteDbContextOptionsBuilder>? sqliteOptionsAction = null)
        {
            ArgumentNullException.ThrowIfNull(connection);

            var extension = (CalciteOptionsExtension)GetOrCreateExtension(optionsBuilder).WithConnection(connection, contextOwnsConnection);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            sqliteOptionsAction?.Invoke(new CalciteDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// Configures the context to connect to a Calcite connection, but without initially setting any
        /// <see cref="DbConnection" /> or connection string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The connection or connection string must be set before the <see cref="DbContext" /> is used to connect
        ///         to a database. Set a connection using <see cref="RelationalDatabaseFacadeExtensions.SetDbConnection" />.
        ///         Set a connection string using <see cref="RelationalDatabaseFacadeExtensions.SetConnectionString" />.
        ///     </para>
        /// </remarks>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional Calcite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder<TContext> UseCalcite<TContext>(
            this DbContextOptionsBuilder<TContext> optionsBuilder,
            Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseCalcite(
                (DbContextOptionsBuilder)optionsBuilder, calciteOptionsAction);

        /// <summary>
        /// Configures the context to connect to a Calcite connection.
        /// </summary>
        /// <typeparam name="TContext">The type of context to be configured.</typeparam>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="connectionString">The connection string of the database to connect to.</param>
        /// <param name="calciteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder<TContext> UseCalcite<TContext>(
            this DbContextOptionsBuilder<TContext> optionsBuilder,
            string? connectionString,
            Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseCalcite(
                (DbContextOptionsBuilder)optionsBuilder, connectionString, calciteOptionsAction);

        /// <summary>
        /// Configures the context to connect to a Calcite connection.
        /// </summary>
        /// <typeparam name="TContext">The type of context to be configured.</typeparam>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="connection">
        /// An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        /// in the open state then EF will not open or close the connection. If the connection is in the closed
        /// state then EF will open and close the connection as needed. The caller owns the connection and is
        /// responsible for its disposal.
        /// </param>
        /// <param name="calciteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder<TContext> UseCalcite<TContext>(
            this DbContextOptionsBuilder<TContext> optionsBuilder,
            DbConnection connection,
            Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseSqlite(
                (DbContextOptionsBuilder)optionsBuilder, connection, calciteOptionsAction);

        /// <summary>
        /// Configures the context to connect to a Calcite database.
        /// </summary>
        /// <typeparam name="TContext">The type of context to be configured.</typeparam>
        /// <param name="optionsBuilder">The builder being used to configure the context.</param>
        /// <param name="connection">
        /// An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        /// in the open state then EF will not open or close the connection. If the connection is in the closed
        /// state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="contextOwnsConnection">
        /// If <see langword="true" />, then EF will take ownership of the connection and will
        /// dispose it in the same way it would dispose a connection created by EF. If <see langword="false" />, then the caller still
        /// owns the connection and is responsible for its disposal.
        /// </param>
        /// <param name="calciteOptionsAction">An optional action to allow additional SQLite specific configuration.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static DbContextOptionsBuilder<TContext> UseCalcite<TContext>(
            this DbContextOptionsBuilder<TContext> optionsBuilder,
            DbConnection connection,
            bool contextOwnsConnection,
            Action<CalciteDbContextOptionsBuilder>? calciteOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseCalcite(
                (DbContextOptionsBuilder)optionsBuilder, connection, contextOwnsConnection, calciteOptionsAction);

        static CalciteOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder options)
            => options.Options.FindExtension<CalciteOptionsExtension>()
                ?? new CalciteOptionsExtension();

        static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsExtension
                = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()
                ?? new CoreOptionsExtension();

            coreOptionsExtension = RelationalOptionsExtension.WithDefaultWarningConfiguration(coreOptionsExtension);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        }

    }

}
