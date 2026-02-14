using Microsoft.EntityFrameworkCore.Query.Associations.OwnedNavigations;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedNavigations
{

    /// <inheritdoc/>
    public class OwnedNavigationsSetOperationsCalciteTest : OwnedNavigationsSetOperationsRelationalTestBase<OwnedNavigationsCalciteFixture>
    {

        /// <summary>
        /// Initializes a new instnace.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="testOutputHelper"></param>
        public OwnedNavigationsSetOperationsCalciteTest(OwnedNavigationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
            base(fixture, testOutputHelper)
        {

        }

    }

}
