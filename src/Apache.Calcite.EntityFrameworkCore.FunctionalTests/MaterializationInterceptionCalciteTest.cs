using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ModelBuilding;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class MaterializationInterceptionCalciteTest(NonSharedFixture fixture) :
    MaterializationInterceptionTestBase<MaterializationInterceptionCalciteTest.CalciteLibraryContext>(fixture)
{

    public class CalciteLibraryContext(DbContextOptions options) : LibraryContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestEntity30244>().OwnsMany(e => e.Settings, b => b.ToJson());
        }

    }

    protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

}

