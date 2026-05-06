using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestModels.Northwind
{

    public class NorthwindCalciteContext : NorthwindRelationalContext
    {

        class Sequence
        {

            [Key]
            [Column("NextValue")]
            public string Name { get; set; }

            [Column("NextValue")]
            public long NextValue { get; set; }

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        public NorthwindCalciteContext(DbContextOptions options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sequence>().HasEntitySequence("EmployeeSequence", e => e.NextValue);
            modelBuilder.Entity<Employee>().Property(p => p.LastName).IsRequired(true);
            modelBuilder.Entity<Employee>().Property(p => p.EmployeeID).UseHiLoEntitySequence("EmployeeSequence");
            modelBuilder.Entity<CustomerQuery>().ToSqlQuery(@"SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region"" FROM ""Customers"" AS ""c""");
        }

    }

}
