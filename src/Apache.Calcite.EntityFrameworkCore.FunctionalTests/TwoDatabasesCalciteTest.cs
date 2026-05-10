// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

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

