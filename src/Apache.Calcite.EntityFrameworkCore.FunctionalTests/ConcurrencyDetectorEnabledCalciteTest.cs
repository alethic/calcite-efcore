using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class ConcurrencyDetectorEnabledCalciteTest(ConcurrencyDetectorEnabledCalciteTest.ConcurrencyDetectorSqlServerFixture fixture)
    : ConcurrencyDetectorEnabledRelationalTestBase<
        ConcurrencyDetectorEnabledCalciteTest.ConcurrencyDetectorSqlServerFixture>(fixture)
{
    public class ConcurrencyDetectorSqlServerFixture : ConcurrencyDetectorFixtureBase, ITestSqlLoggerFactory
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;
    }
}

