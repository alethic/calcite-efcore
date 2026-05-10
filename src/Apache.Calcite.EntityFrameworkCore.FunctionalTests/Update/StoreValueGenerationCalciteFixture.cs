using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Update;

public class StoreValueGenerationCalciteFixture : StoreValueGenerationFixtureBase
{

    protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

}

