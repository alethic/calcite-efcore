using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.Tests.HiLo;

using IKVM.Jdbc.Data;

using java.sql;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.apache.calcite.adapter.java;
using org.apache.calcite.jdbc;
using org.apache.calcite.sql.validate;

namespace Apache.Calcite.EntityFrameworkCore.Tests.Storage
{

    /// <summary>
    /// Tests covering <see cref="IRelationalDatabaseCreator"/> (CalciteDatabaseCreator) behavior in isolation.
    /// These tests deliberately do not depend on EnsureCreated so failures pinpoint the creator itself.
    /// </summary>
    [TestClass]
    public class CalciteDatabaseCreatorTests
    {

        static readonly org.apache.calcite.jdbc.Driver CalciteJdbcDriver;
        static readonly CalciteJdbc41Factory CalciteJdbc41Factory;
        static readonly MethodInfo NewConnectionMethod;

        static CalciteDatabaseCreatorTests()
        {
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.server.ServerDdlExecutor).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.joou.ULong).Assembly);

            CalciteJdbcDriver = (org.apache.calcite.jdbc.Driver)DriverManager.getDriver("jdbc:calcite:");
            CalciteJdbc41Factory = new CalciteJdbc41Factory();
            NewConnectionMethod = typeof(CalciteJdbc41Factory)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .First(i => i.Name == nameof(CalciteJdbc41Factory.newConnection) && i.GetParameters().Length == 6);
        }

        static Connection InvokeNewConnection(string url, java.util.Properties info, CalciteSchema rootSchema, JavaTypeFactory typeFactory)
        {
            return (Connection?)NewConnectionMethod.Invoke(CalciteJdbc41Factory, [CalciteJdbcDriver, CalciteJdbc41Factory, url, info, rootSchema, typeFactory])
                ?? throw new InvalidOperationException();
        }

        static JdbcConnection CreateConnection(string schema)
        {
            var properties = new java.util.Properties();
            properties.setProperty("schema", schema);
            properties.setProperty("conformance", SqlConformanceEnum.LENIENT.name());
            properties.setProperty("parserFactory", "org.apache.calcite.server.ServerDdlExecutor#PARSER_FACTORY");

            using var tmp = DriverManager.getConnection("jdbc:calcite:");
            var calcite = (CalciteConnection)tmp.unwrap(typeof(CalciteConnection));
            var rootSchema = CalciteSchema.from(calcite.getRootSchema());
            var typeFactory = calcite.getTypeFactory();

            return new JdbcConnection(() => InvokeNewConnection("jdbc:calcite:", properties, rootSchema, typeFactory));
        }

        static (JdbcConnection Connection, HiLoDbContext Context, IRelationalDatabaseCreator Creator) CreateContextAndCreator()
        {
            var schema = "S" + Guid.NewGuid().ToString("N");
            var conn = CreateConnection(schema);
            var ctx = new HiLoDbContext(conn, schema);
            var creator = (IRelationalDatabaseCreator)ctx.GetService<IDatabaseCreator>();
            return (conn, ctx, creator);
        }

        [TestMethod]
        public void Resolved_creator_is_calcite_creator()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsNotNull(creator);
                Assert.AreEqual(
                    "Apache.Calcite.EntityFrameworkCore.Storage.Internal.CalciteDatabaseCreator",
                    creator.GetType().FullName);
            }
        }

        [TestMethod]
        public void Exists_returns_true()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsTrue(creator.Exists());
            }
        }

        [TestMethod]
        public async Task ExistsAsync_returns_true()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsTrue(await creator.ExistsAsync());
            }
        }

        [TestMethod]
        public void HasTables_on_fresh_connection_returns_false()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                // No DDL has been executed against this Calcite root schema, so HasTables must return
                // false. If this returns true, EnsureCreated will short-circuit and never create our
                // model's tables.
                Assert.IsFalse(creator.HasTables(),
                    "HasTables on a brand new Calcite connection must be false (only system tables exist).");
            }
        }

        [TestMethod]
        public async Task HasTablesAsync_on_fresh_connection_returns_false()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsFalse(await creator.HasTablesAsync(),
                    "HasTablesAsync on a brand new Calcite connection must be false (only system tables exist).");
            }
        }

        [TestMethod]
        public void HasTables_does_not_count_system_tables()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                // Sanity: confirm the metadata schema itself contains rows so we know the underlying
                // query is reachable. HasTables must still report false because those rows are SYSTEM.
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM \"metadata\".\"TABLES\"";
                    var total = Convert.ToInt64(cmd.ExecuteScalar());
                    Assert.IsTrue(total > 0, "metadata.TABLES should expose at least the Calcite system tables.");
                }

                Assert.IsFalse(creator.HasTables(),
                    "HasTables must filter out SYSTEM TABLE rows in metadata.TABLES.");
            }
        }

        static System.Collections.Generic.List<string> ListUserTables(JdbcConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT \"tableSchem\", \"tableName\" FROM \"metadata\".\"TABLES\" WHERE \"tableType\" = 'TABLE'";

            var found = new System.Collections.Generic.List<string>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = reader.IsDBNull(0) ? "<null>" : reader.GetString(0);
                var n = reader.IsDBNull(1) ? "<null>" : reader.GetString(1);
                found.Add($"{s}.{n}");
            }

            return found;
        }

        [TestMethod]
        public void GenerateCreateScript_includes_model_tables()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                var script = creator.GenerateCreateScript();

                Assert.IsFalse(string.IsNullOrWhiteSpace(script),
                    "GenerateCreateScript should produce DDL for the model.");
                StringAssert.Contains(script, "CalciteSequence",
                    $"Expected CREATE TABLE for the HiLo backing table. Script:\n{script}");
                StringAssert.Contains(script, "PRODUCTS",
                    $"Expected CREATE TABLE for the PRODUCTS entity. Script:\n{script}");
            }
        }

        [TestMethod]
        public void CreateTables_creates_model_tables()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsFalse(creator.HasTables(),
                    "Pre-condition: no user tables should exist before CreateTables.");

                creator.CreateTables();

                var tables = ListUserTables(conn);
                Assert.IsTrue(
                    tables.Exists(t => t.EndsWith(".CalciteSequence", StringComparison.Ordinal)),
                    $"CreateTables should create the CalciteSequence backing table. metadata.TABLES (TABLE rows): [{string.Join(", ", tables)}]");
                Assert.IsTrue(
                    tables.Exists(t => t.EndsWith(".PRODUCTS", StringComparison.Ordinal)),
                    $"CreateTables should create the PRODUCTS table. metadata.TABLES (TABLE rows): [{string.Join(", ", tables)}]");
            }
        }

        [TestMethod]
        public async Task CreateTablesAsync_creates_model_tables()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsFalse(await creator.HasTablesAsync(),
                    "Pre-condition: no user tables should exist before CreateTablesAsync.");

                await creator.CreateTablesAsync();

                var tables = ListUserTables(conn);
                Assert.IsTrue(
                    tables.Exists(t => t.EndsWith(".CalciteSequence", StringComparison.Ordinal)),
                    $"CreateTablesAsync should create the CalciteSequence backing table. metadata.TABLES (TABLE rows): [{string.Join(", ", tables)}]");
                Assert.IsTrue(
                    tables.Exists(t => t.EndsWith(".PRODUCTS", StringComparison.Ordinal)),
                    $"CreateTablesAsync should create the PRODUCTS table. metadata.TABLES (TABLE rows): [{string.Join(", ", tables)}]");
            }
        }

        [TestMethod]
        public void CreateTables_makes_HasTables_return_true()
        {
            var (conn, ctx, creator) = CreateContextAndCreator();
            using (conn)
            using (ctx)
            {
                Assert.IsFalse(creator.HasTables());
                creator.CreateTables();
                Assert.IsTrue(creator.HasTables(),
                    "After CreateTables, HasTables must report true so EnsureCreated short-circuits subsequent calls.");
            }
        }

    }

}
