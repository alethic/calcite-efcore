using System;
using System.Data.Common;
using System.Threading.Tasks;

using Apache.Calcite.Data;
using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities.NorthwindReflectiveSchema;
using Apache.Calcite.EntityFrameworkCore.Infrastructure;

using IKVM.Jdbc.Data;

using java.sql;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

using org.apache.calcite.adapter.java;
using org.apache.calcite.jdbc;
using org.apache.calcite.sql.validate;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    /// <summary>
    /// <see cref="RelationalTestStore"/> implementation for Calcite's RelationalSchema provider against Northwind.
    /// </summary>
    public partial class ReflectiveSchemaNorthwindTestStore : RelationalTestStore
    {

        static readonly SchemaTarget SchemaTarget = new SchemaTarget();

        /// <summary>
        /// Initializes the static instance.
        /// </summary>
        static ReflectiveSchemaNorthwindTestStore()
        {
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(SchemaTarget).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
        }

        public const int CommandTimeout = 30;

        public static ReflectiveSchemaNorthwindTestStore GetOrCreate(string name)
            => new(name);

        public static async Task<ReflectiveSchemaNorthwindTestStore> GetOrCreateInitializedAsync(string name)
            => await new ReflectiveSchemaNorthwindTestStore(name).InitializeCalciteAsync(
                new ServiceCollection().AddEntityFrameworkCalcite().BuildServiceProvider(validateScopes: true),
                (Func<DbContext>?)null,
                null);

        public static ReflectiveSchemaNorthwindTestStore GetExisting(string name)
            => new(name, seed: false);

        public static ReflectiveSchemaNorthwindTestStore Create(string name)
            => new(name, shared: false);

        readonly bool _seed;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="seed"></param>
        /// <param name="shared"></param>
        private ReflectiveSchemaNorthwindTestStore(string name, bool seed = true, bool shared = true) :
            base(name, shared, CreateConnection(name))
        {
            _seed = seed;
        }

        public virtual DbContextOptionsBuilder AddProviderOptions(
            DbContextOptionsBuilder builder,
            Action<CalciteDbContextOptionsBuilder>? configureSqlite)
            => UseConnectionString
                ? builder.UseCalcite(
                    ConnectionString, b =>
                    {
                        b.CommandTimeout(CommandTimeout);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                        configureSqlite?.Invoke(b);
                    })
                : builder.UseCalcite(
                    (JdbcConnection)Connection, b =>
                    {
                        b.CommandTimeout(CommandTimeout);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                        configureSqlite?.Invoke(b);
                    });

        /// <inheritdoc/>
        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
            => AddProviderOptions(builder, configureSqlite: null);

        public async Task<ReflectiveSchemaNorthwindTestStore> InitializeCalciteAsync(
            IServiceProvider? serviceProvider,
            Func<DbContext>? createContext,
            Func<DbContext, Task>? seed)
            => (ReflectiveSchemaNorthwindTestStore)await InitializeAsync(serviceProvider, createContext, seed);

        public async Task<ReflectiveSchemaNorthwindTestStore> InitializeCalciteAsync(
            IServiceProvider serviceProvider,
            Func<ReflectiveSchemaNorthwindTestStore, DbContext> createContext,
            Func<DbContext, Task> seed)
            => (ReflectiveSchemaNorthwindTestStore)await InitializeAsync(serviceProvider, () => createContext(this), seed);

        protected override async Task InitializeAsync(Func<DbContext> createContext, Func<DbContext, Task>? seed, Func<DbContext, Task>? clean)
        {
            if (!_seed)
            {
                return;
            }

            using var context = createContext();

            var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
            if (await databaseCreator.ExistsAsync())
            {
                if (clean != null)
                {
                    await clean(context);
                }

                await CleanAsync(context);
            }

            // Run context seeding
            await context.Database.EnsureCreatedResilientlyAsync();

            if (seed != null)
            {
                await seed(context);
            }
        }

        public override Task CleanAsync(DbContext context)
        {
            context.Database.EnsureClean();
            return Task.CompletedTask;
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            using var command = CreateCommand(sql, parameters);
            return command.ExecuteNonQuery();
        }

        public T ExecuteScalar<T>(string sql, params object[] parameters)
        {
            using var command = CreateCommand(sql, parameters);
            return (T)command.ExecuteScalar()!;
        }

        DbCommand CreateCommand(string commandText, object[] parameters)
        {
            var command = (JdbcCommand)Connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandTimeout = CommandTimeout;

            for (var i = 0; i < parameters.Length; i++)
                command.Parameters.AddWithValue(i.ToString(), parameters[i]);

            return command;
        }

        static JdbcConnection CreateConnection(string name)
        {
            var p = new java.util.Properties();
            var s = new CalciteConnectionProperties(p);
            s.Schema = "NORTHWIND";
            s.Conformance = SqlConformanceEnum.DEFAULT;

            var connection = DriverManager.getConnection("jdbc:calcite:", p);
            var calciteConnection = (CalciteConnection)connection.unwrap(typeof(CalciteConnection));
            var rootSchema = calciteConnection.getRootSchema();
            var schema = new ReflectiveSchema(SchemaTarget);
            rootSchema.add("NORTHWIND", schema);
            calciteConnection.setSchema("NORTHWIND");

            return new JdbcConnection(connection);
        }

    }

}
