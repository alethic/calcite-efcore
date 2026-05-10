using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class IncludeOneToOneCalciteTest(IncludeOneToOneCalciteTest.OneToOneQueryCalciteFixture fixture) :
    IncludeOneToOneTestBase<IncludeOneToOneCalciteTest.OneToOneQueryCalciteFixture>(fixture)
{
    public class OneToOneQueryCalciteFixture : OneToOneQueryFixtureBase, ITestSqlLoggerFactory
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

        public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

    }
}

