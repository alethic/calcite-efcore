using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Apache.Calcite.EntityFrameworkCore.Tests.Csv
{

    public class CsvDbContext : DbContext
    {

        /// <summary>
        /// Initializes the static instance.
        /// </summary>
        static CsvDbContext()
        {
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.adapter.csv.CsvSchemaFactory).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
        }

        public DbSet<CsvEmployee> Employees { get; set; }

        public DbSet<CsvDepartment> Departments { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsvEmployee>();
            modelBuilder.Entity<CsvDepartment>();
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCalcite("jdbc:calcite:model=Csv/model.json", o =>
            {

            });
        }

    }

}
