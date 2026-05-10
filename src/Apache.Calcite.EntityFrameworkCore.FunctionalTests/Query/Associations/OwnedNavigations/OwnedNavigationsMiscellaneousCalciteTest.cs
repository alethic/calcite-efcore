using Microsoft.EntityFrameworkCore.Query.Associations.OwnedNavigations;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedNavigations
{

    public class OwnedNavigationsMiscellaneousCalciteTest : OwnedNavigationsMiscellaneousRelationalTestBase<OwnedNavigationsCalciteFixture>
    {

        public OwnedNavigationsMiscellaneousCalciteTest(OwnedNavigationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
            base(fixture, testOutputHelper)
        {

        }

    }

}
