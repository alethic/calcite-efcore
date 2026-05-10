using Microsoft.EntityFrameworkCore.Query.Associations.Navigations;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.Navigations
{

    public class NavigationsSetOperationsCalciteTest : NavigationsSetOperationsRelationalTestBase<NavigationsCalciteFixture>
    {

        public NavigationsSetOperationsCalciteTest(NavigationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
            base(fixture, testOutputHelper)
        {

        }

    }

}
