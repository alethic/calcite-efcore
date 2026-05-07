using System;
using System.Collections.Concurrent;
using System.Linq;

using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.Extensions;

using ikvm.runtime;

using IKVM.Jdbc.Data;

using java.sql;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

using org.apache.calcite.adapter.java;
using org.apache.calcite.avatica;
using org.apache.calcite.jdbc;
using org.apache.calcite.sql.validate;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    /// <summary>
    /// <see cref="RelationalTestStore"/> implementation for Calcite's RelationalSchema provider against Northwind.
    /// </summary>
    public class CalciteTestStore : RelationalTestStore
    {

        public const int CommandTimeout = 30;

        readonly record struct CalciteStoreInstance(java.util.Properties Properties, CalciteSchema RootSchema, JavaTypeFactory TypeFactory);

        static readonly org.apache.calcite.jdbc.Driver CalciteJdbcDriver;
        static readonly CalciteJdbc41Factory CalciteJdbc41Factory;
        static readonly MethodInfo newConnectionMethodInfo;

        /// <summary>
        /// Initializes the static instance.
        /// </summary>
        static CalciteTestStore()
        {
            Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
            Startup.addBootClassPathAssembly(typeof(org.apache.calcite.server.ServerDdlExecutor).Assembly);
            Startup.addBootClassPathAssembly(typeof(org.joou.ULong).Assembly);

            CalciteJdbcDriver = (org.apache.calcite.jdbc.Driver)DriverManager.getDriver("jdbc:calcite:");
            CalciteJdbc41Factory = new CalciteJdbc41Factory();
            newConnectionMethodInfo = typeof(CalciteJdbc41Factory).GetMethods(BindingFlags.Public | BindingFlags.Instance).First(i => i.Name == nameof(CalciteJdbc41Factory.newConnection) && i.GetParameters().Length == 6);
        }

        /// <summary>
        /// Invokes the 'newConnection' method on <see cref="CalciteJdbc41Factory"/>. This method has two identical versions and cannot be resolved by C#.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="factory"></param>
        /// <param name="url"></param>
        /// <param name="info"></param>
        /// <param name="rootSchema"></param>
        /// <param name="typeFactory"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        static Connection InvokeNewConnection(UnregisteredDriver driver, AvaticaFactory factory, string url, java.util.Properties info, CalciteSchema rootSchema, JavaTypeFactory typeFactory)
        {
            return (Connection?)newConnectionMethodInfo.Invoke(CalciteJdbc41Factory, [driver, factory, url, info, rootSchema, typeFactory]) ?? throw new InvalidOperationException();
        }

        static readonly ConcurrentDictionary<string, CalciteStoreInstance> _shared = new();

        /// <summary>
        /// Creates a new JDBC Calcite connection.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static CalciteStoreInstance CreateStoreInstance(string name)
        {
            var properties = new java.util.Properties();
            properties.setProperty("schema", name);
            properties.setProperty("conformance", SqlConformanceEnum.LENIENT.name());
            properties.setProperty("parserFactory", "org.apache.calcite.server.ServerDdlExecutor#PARSER_FACTORY");

            using var tmpConnection = DriverManager.getConnection("jdbc:calcite:");
            var tmpCalciteConnection = (CalciteConnection)tmpConnection.unwrap(typeof(CalciteConnection));
            var rootSchema = CalciteSchema.from(tmpCalciteConnection.getRootSchema());

            return new CalciteStoreInstance(properties, rootSchema, tmpCalciteConnection.getTypeFactory());
        }

        /// <summary>
        /// Creates a new JDBC Calcite connection to the specified store instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        static JdbcConnection CreateConnection(CalciteStoreInstance instance)
        {
            return new JdbcConnection(() => InvokeNewConnection(CalciteJdbcDriver, CalciteJdbc41Factory, "jdbc:calcite:", instance.Properties, instance.RootSchema, instance.TypeFactory));
        }

        /// <summary>
        /// Creates the specified store.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CalciteTestStore Create(string name)
        {
            return new CalciteTestStore(name, false, CreateConnection(CreateStoreInstance(name)));
        }

        /// <summary>
        /// Gets or creates the specified store 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CalciteTestStore GetOrCreate(string name)
        {
            return new CalciteTestStore(name, true, CreateConnection(_shared.GetOrAdd(name, CreateStoreInstance)));
        }

        readonly string? _initScript;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shared"></param>
        /// <param name="connection"></param>
        /// <param name="initScript"></param>
        protected CalciteTestStore(string name, bool shared, JdbcConnection connection, string? initScript = null) :
            base(name, shared, connection)
        {
            _initScript = initScript;
        }

        /// <inheritdoc/>
        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
        {
            if (UseConnectionString)
            {
                return builder.UseCalcite(ConnectionString, b =>
                {
                    b.CommandTimeout(CommandTimeout);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            }
            else
            {
                if (Connection is not JdbcConnection connection)
                    throw new InvalidOperationException("Calcite Provider must be provided a JdbcConnection.");

                return builder.UseCalcite(connection, b =>
                {
                    b.CommandTimeout(CommandTimeout);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            }
        }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(Func<DbContext> createContext, Func<DbContext, Task>? seed, Func<DbContext, Task>? clean)
        {
            using var context = createContext();

            if (clean != null)
            {
                await clean(context);
            }

            await CleanAsync(context);

            if (seed != null)
            {
                await seed(context);
            }
        }

        /// <inheritdoc/>
        public override async Task CleanAsync(DbContext context)
        {
            context.Database.EnsureClean();

            if (_initScript is not null)
            {
                await context.Database.ExecuteScriptAsync(_initScript, CancellationToken.None);
            }
        }

    }

}
