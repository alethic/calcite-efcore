using System.Data.Common;

using Apache.Calcite.Data;
using Apache.Calcite.EntityFrameworkCore.Extensions;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore;

namespace Apache.Calcite.EntityFrameworkCore.Tests.HiLo
{

    public class HiLoDbContext : DbContext
    {

        readonly DbConnection _connection;
        readonly string _schema;

        public HiLoDbContext(JdbcConnection connection, string schema)
        {
            _connection = connection;
            _schema = schema;
        }

        public HiLoDbContext(CalciteConnection connection, string schema)
        {
            _connection = connection;
            _schema = schema;
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);
            modelBuilder.UseHiLoEntitySequence();
            modelBuilder.Entity<Product>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (_connection)
            {
                case CalciteConnection calcite:
                    optionsBuilder.UseCalcite(calcite, b => b.MaxBatchSize(1));
                    break;
                case JdbcConnection jdbc:
                    optionsBuilder.UseCalcite(jdbc, b => b.MaxBatchSize(1));
                    break;
                default:
                    throw new System.InvalidOperationException("Unsupported connection type.");
            }
        }

    }

}
