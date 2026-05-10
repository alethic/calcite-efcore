using Apache.Calcite.Data;
using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Apache.Calcite.EntityFrameworkCore.Tests.HiLo
{

    public class HiLoDbContext : DbContext
    {

        readonly CalciteConnection _connection;
        readonly string _schema;

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
            optionsBuilder.UseCalcite(_connection, b => b.MaxBatchSize(1));
        }

    }

}
