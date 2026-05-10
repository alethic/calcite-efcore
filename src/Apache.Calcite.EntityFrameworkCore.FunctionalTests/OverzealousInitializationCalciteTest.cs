using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class OverzealousInitializationCalciteTest(OverzealousInitializationCalciteTest.OverzealousInitializationCalciteFixture fixture)
    : OverzealousInitializationTestBase<OverzealousInitializationCalciteTest.OverzealousInitializationCalciteFixture>(fixture)
{
    public class OverzealousInitializationCalciteFixture : OverzealousInitializationFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

