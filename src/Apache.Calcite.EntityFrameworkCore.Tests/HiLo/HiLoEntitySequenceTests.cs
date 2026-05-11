using System;
using System.Linq;
using System.Threading.Tasks;

using Apache.Calcite.Data;

using Xunit;

namespace Apache.Calcite.EntityFrameworkCore.Tests.HiLo
{

    /// <summary>
    /// Tests covering the EntitySequenceHiLo value-generation strategy against a live in-memory Calcite store.
    /// </summary>
    public class HiLoEntitySequenceTests
    {

        static HiLoEntitySequenceTests()
        {
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.server.ServerDdlExecutor).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.joou.ULong).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.linq4j.tree.BlockBuilder).Assembly);
        }

        /// <summary>
        /// Creates a fresh in-memory Calcite JDBC connection backed by a unique schema.
        /// </summary>
        static CalciteConnection CreateConnection(string schema)
        {
            var str = new CalciteConnectionStringBuilder();
            str.Schema = schema;
            str.Conformance = "LENIENT";
            str["parserFactory"] = "org.apache.calcite.server.ServerDdlExecutor#PARSER_FACTORY";
            return new CalciteConnection(str.ToString());
        }

        static async Task<(CalciteConnection Connection, HiLoDbContext Context, string Schema)> CreateAndInitializeAsync()
        {
            var schema = "S" + Guid.NewGuid().ToString("N");
            var conn = CreateConnection(schema);
            var ctx = new HiLoDbContext(conn, schema);
            await ctx.Database.EnsureCreatedAsync();
            return (conn, ctx, schema);
        }

        [Fact]
        public async Task Sequence_table_is_created()
        {
            var (conn, ctx, _) = await CreateAndInitializeAsync();
            using (conn)
            using (ctx)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                var schema = conn.Database;

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT \"tableSchem\", \"tableName\", \"tableType\" FROM \"metadata\".\"TABLES\"";

                var found = new System.Collections.Generic.List<string>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var s = reader.IsDBNull(0) ? "<null>" : reader.GetString(0);
                        var n = reader.IsDBNull(1) ? "<null>" : reader.GetString(1);
                        var t = reader.IsDBNull(2) ? "<null>" : reader.GetString(2);
                        found.Add($"{s}.{n} ({t})");
                    }
                }

                Assert.True(
                    found.Exists(t => t.Contains(".CalciteSequence ")),
                    $"EnsureCreated should have created the CalciteSequence backing table in schema '{schema}'. All metadata.TABLES rows: [{string.Join(", ", found)}]");
            }
        }

        [Fact]
        public async Task Can_insert_with_generated_keys()
        {
            var (conn, ctx, _) = await CreateAndInitializeAsync();
            using (conn)
            using (ctx)
            {
                ctx.Products.Add(new Product { Name = "Widget", Price = 9.99m });
                ctx.Products.Add(new Product { Name = "Gadget", Price = 19.99m });
                await ctx.SaveChangesAsync();

                Assert.Equal(2, ctx.Products.Count());
                foreach (var p in ctx.Products)
                    Assert.True(p.Id > 0, "HiLo sequence should have produced a non-zero key.");
            }
        }

        [Fact]
        public async Task Generated_keys_are_unique()
        {
            var (conn, ctx, _) = await CreateAndInitializeAsync();
            using (conn)
            using (ctx)
            {
                for (int i = 0; i < 5; i++)
                    ctx.Products.Add(new Product { Name = $"P{i}", Price = i });

                await ctx.SaveChangesAsync();

                var ids = ctx.Products.Select(p => p.Id).ToList();
                Assert.Distinct(ids);
                Assert.True(ids.TrueForAll(id => id > 0));
            }
        }

        [Fact]
        public async Task Can_select_inserted_rows()
        {
            var (conn, ctx, _) = await CreateAndInitializeAsync();
            using (conn)
            using (ctx)
            {
                ctx.Products.Add(new Product { Name = "Alpha", Price = 1m });
                ctx.Products.Add(new Product { Name = "Beta", Price = 2m });
                ctx.Products.Add(new Product { Name = "Gamma", Price = 3m });
                await ctx.SaveChangesAsync();

                var names = ctx.Products.OrderBy(p => p.Name).Select(p => p.Name).ToList();
                Assert.Equal(new[] { "Alpha", "Beta", "Gamma" }, names);

                var beta = ctx.Products.Single(p => p.Name == "Beta");
                Assert.Equal(2m, beta.Price);
            }
        }

        [Fact]
        public async Task Can_update_inserted_rows()
        {
            var (conn, ctx, schema) = await CreateAndInitializeAsync();
            using (conn)
            using (ctx)
            {
                var product = new Product { Name = "Original", Price = 5m };
                ctx.Products.Add(product);
                await ctx.SaveChangesAsync();

                var id = product.Id;
                Assert.True(id > 0);

                product.Name = "Updated";
                product.Price = 7.5m;
                await ctx.SaveChangesAsync();
            }

            using (conn)
            using (var verify = new HiLoDbContext(conn, schema))
            {
                var loaded = verify.Products.Single();
                Assert.Equal("Updated", loaded.Name);
                Assert.Equal(7.5m, loaded.Price);
            }
        }

    }

}
