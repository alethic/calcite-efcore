using Microsoft.EntityFrameworkCore.Query.Associations.OwnedNavigations;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedNavigations
{

    public class OwnedNavigationsStructuralEqualityCalciteTest : OwnedNavigationsStructuralEqualityRelationalTestBase<OwnedNavigationsCalciteFixture>
    {

        public OwnedNavigationsStructuralEqualityCalciteTest(OwnedNavigationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
            base(fixture, testOutputHelper)
        {

        }

    }

}
