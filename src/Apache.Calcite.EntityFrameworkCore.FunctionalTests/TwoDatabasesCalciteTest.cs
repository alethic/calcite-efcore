using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class TwoDatabasesCalciteTest(TwoDatabasesCalciteTest.TwoDatabasesFixture fixture)
    : TwoDatabasesTestBase(fixture), IClassFixture<TwoDatabasesCalciteTest.TwoDatabasesFixture>
{
    protected new TwoDatabasesFixture Fixture
        => (TwoDatabasesFixture)base.Fixture;

    protected override DbContextOptionsBuilder CreateTestOptions(
        DbContextOptionsBuilder optionsBuilder,
        bool withConnectionString = false,
        bool withNullConnectionString = false)
        => withConnectionString
            ? withNullConnectionString
                ? optionsBuilder.UseCalcite((string)null)
                : optionsBuilder.UseCalcite(DummyConnectionString)
            : optionsBuilder.UseCalcite();

    protected override TwoDatabasesWithDataContext CreateBackingContext(string databaseName)
        => new(Fixture.CreateOptions(CalciteTestStore.Create(databaseName)));

    protected override string DummyConnectionString
        => "DataSource=DummyDatabase";

    public class TwoDatabasesFixture : ServiceProviderFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

