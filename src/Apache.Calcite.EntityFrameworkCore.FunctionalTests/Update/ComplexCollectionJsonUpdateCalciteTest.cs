using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Update;

public class ComplexCollectionJsonUpdateCalciteTest : ComplexCollectionJsonUpdateTestBase<ComplexCollectionJsonUpdateCalciteTest.ComplexCollectionJsonUpdateCalciteFixture>
{

    public ComplexCollectionJsonUpdateCalciteTest(ComplexCollectionJsonUpdateCalciteFixture fixture) :
        base(fixture)
    {
        ClearLog();
    }

    public class ComplexCollectionJsonUpdateCalciteFixture : ComplexCollectionJsonUpdateFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;


    }

}

