using System;
using System.Data.Common;
using System.Threading.Tasks;

using Apache.Calcite.Data;
using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Infrastructure;

using IKVM.Jdbc.Data;

using java.util;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public class CalciteTestStore : RelationalTestStore
    {

        /// <summary>
        /// Initializes the static instance.
        /// </summary>
        static CalciteTestStore()
        {
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.sqlite.JDBC).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
        }

        public const int CommandTimeout = 30;

        public static CalciteTestStore GetOrCreate(string name)
            => new(name);

        public static async Task<CalciteTestStore> GetOrCreateInitializedAsync(string name)
            => await new CalciteTestStore(name).InitializeSqliteAsync(
                new ServiceCollection().AddEntityFrameworkCalcite().BuildServiceProvider(validateScopes: true),
                (Func<DbContext>?)null,
                null);

        public static CalciteTestStore GetExisting(string name)
            => new(name, seed: false);

        public static CalciteTestStore Create(string name)
            => new(name, shared: false);

        private readonly bool _seed;

        private CalciteTestStore(string name, bool seed = true, bool shared = true)
            : base(name, shared, CreateConnection(name))
            => _seed = seed;

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

        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
            => AddProviderOptions(builder, configureSqlite: null);

        public async Task<CalciteTestStore> InitializeSqliteAsync(
            IServiceProvider? serviceProvider,
            Func<DbContext>? createContext,
            Func<DbContext, Task>? seed)
            => (CalciteTestStore)await InitializeAsync(serviceProvider, createContext, seed);

        public async Task<CalciteTestStore> InitializeSqliteAsync(
            IServiceProvider serviceProvider,
            Func<CalciteTestStore, DbContext> createContext,
            Func<DbContext, Task> seed)
            => (CalciteTestStore)await InitializeAsync(serviceProvider, () => createContext(this), seed);

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

        private DbCommand CreateCommand(string commandText, object[] parameters)
        {
            var command = (JdbcCommand)Connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandTimeout = CommandTimeout;

            for (var i = 0; i < parameters.Length; i++)
            {
                command.Parameters.AddWithValue("@p" + i, parameters[i]);
            }

            return command;
        }

        private static JdbcConnection CreateConnection(string name)
        {
            var p = new java.util.Properties();
            var s = new CalciteConnectionProperties(p);
            s.Schema = "NORTHWIND";
            s.SchemaFactory = "org.apache.calcite.adapter.jdbc.JdbcSchema$Factory";
            s.SchemaType = "JDBC";
            s.SchemaProperties["jdbcDriver"] = "org.sqlite.JDBC";
            s.SchemaProperties["jdbcUrl"] = $"jdbc:sqlite:{name}.db";
            s.SchemaProperties["jdbcSchema"] = "public";
            return new JdbcConnection("jdbc:calcite:", p.AsDictionary<string, string>());
        }

    }

}
