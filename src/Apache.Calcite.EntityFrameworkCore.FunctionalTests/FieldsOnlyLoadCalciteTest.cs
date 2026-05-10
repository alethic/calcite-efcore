using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class FieldsOnlyLoadCalciteTest(FieldsOnlyLoadCalciteTest.FieldsOnlyLoadCalciteFixture fixture) :
    FieldsOnlyLoadTestBase<FieldsOnlyLoadCalciteTest.FieldsOnlyLoadCalciteFixture>(fixture)
{

    public class FieldsOnlyLoadCalciteFixture : FieldsOnlyLoadFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}
