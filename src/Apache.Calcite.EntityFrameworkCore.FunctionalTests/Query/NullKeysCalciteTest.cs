using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class NullKeysCalciteTest(NullKeysCalciteTest.NullKeysCalciteFixture fixture)
    : NullKeysTestBase<NullKeysCalciteTest.NullKeysCalciteFixture>(fixture)
{
    public class NullKeysCalciteFixture : NullKeysFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

