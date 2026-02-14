using Microsoft.EntityFrameworkCore.Query.Associations.Navigations;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.Navigations
{

    /// <inheritdoc/>
    public class NavigationsStructuralEqualityCalciteTest : NavigationsStructuralEqualityRelationalTestBase<NavigationsCalciteFixture>
    {

        /// <summary>
        /// Initializes a new instnace.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="testOutputHelper"></param>
        public NavigationsStructuralEqualityCalciteTest(NavigationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
            base(fixture, testOutputHelper)
        {

        }

    }

}
