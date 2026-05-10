using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class LoadCalciteTest(LoadCalciteTest.LoadCalciteFixture fixture) : LoadTestBase<LoadCalciteTest.LoadCalciteFixture>(fixture)
{

    public class LoadCalciteFixture : LoadFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

