using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Update;

public class UpdatesCalciteTest(UpdatesCalciteTest.UpdatesCalciteFixture fixture) :
    UpdatesRelationalTestBase<UpdatesCalciteTest.UpdatesCalciteFixture>(fixture)
{

    public override void Identifiers_are_generated_correctly()
    {
        throw new System.NotImplementedException();
    }

    public class UpdatesCalciteFixture : UpdatesRelationalFixture
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

