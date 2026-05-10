using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class ConcurrencyDetectorDisabledCalciteTest(ConcurrencyDetectorDisabledCalciteTest.ConcurrencyDetectorSqlServerFixture fixture)
    : ConcurrencyDetectorDisabledRelationalTestBase<
        ConcurrencyDetectorDisabledCalciteTest.ConcurrencyDetectorSqlServerFixture>(fixture)
{
    public class ConcurrencyDetectorSqlServerFixture : ConcurrencyDetectorFixtureBase, ITestSqlLoggerFactory
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder)
                .EnableThreadSafetyChecks(enableChecks: false);
    }
}

