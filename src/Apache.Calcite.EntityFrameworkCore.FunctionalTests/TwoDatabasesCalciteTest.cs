using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class TwoDatabasesCalciteTest(TwoDatabasesCalciteTest.TwoDatabasesFixture fixture) :
    TwoDatabasesTestBase(fixture),
    IClassFixture<TwoDatabasesCalciteTest.TwoDatabasesFixture>
{

    protected new TwoDatabasesFixture Fixture => (TwoDatabasesFixture)base.Fixture;

    protected override string DummyConnectionString => throw new System.NotImplementedException();

    protected override TwoDatabasesWithDataContext CreateBackingContext(string databaseName)
    {
        throw new System.NotImplementedException();
    }

    protected override DbContextOptionsBuilder CreateTestOptions(DbContextOptionsBuilder optionsBuilder, bool withConnectionString = false, bool withNullConnectionString = false)
    {
        throw new System.NotImplementedException();
    }

    public class TwoDatabasesFixture : ServiceProviderFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

