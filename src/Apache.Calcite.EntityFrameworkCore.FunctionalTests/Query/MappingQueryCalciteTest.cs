using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class MappingQueryCalciteTest(MappingQueryCalciteTest.MappingQueryCalciteFixture fixture)
    : MappingQueryTestBase<MappingQueryCalciteTest.MappingQueryCalciteFixture>(fixture)
{

    public class MappingQueryCalciteFixture : MappingQueryFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

        protected override string DatabaseSchema { get; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<MappedCustomer>(e =>
            {
                e.Property(c => c.CompanyName2).Metadata.SetColumnName("CompanyName");
                e.Metadata.SetTableName("Customers");
            });
        }

    }

}

