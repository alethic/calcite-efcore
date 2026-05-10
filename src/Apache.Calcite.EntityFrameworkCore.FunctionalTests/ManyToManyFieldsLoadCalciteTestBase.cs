using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class ManyToManyFieldsLoadCalciteTest(ManyToManyFieldsLoadCalciteTest.ManyToManyFieldsLoadCalciteFixture fixture) :
    ManyToManyFieldsLoadTestBase<ManyToManyFieldsLoadCalciteTest.ManyToManyFieldsLoadCalciteFixture>(fixture)
{

    public class ManyToManyFieldsLoadCalciteFixture : ManyToManyFieldsLoadFixtureBase, ITestSqlLoggerFactory
    {

        public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

