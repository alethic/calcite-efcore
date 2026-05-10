using Microsoft.EntityFrameworkCore.Query.Associations.Navigations;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.Navigations
{

    public class NavigationsStructuralEqualityCalciteTest : NavigationsStructuralEqualityRelationalTestBase<NavigationsCalciteFixture>
    {

        public NavigationsStructuralEqualityCalciteTest(NavigationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
            base(fixture, testOutputHelper)
        {

        }

    }

}
