using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class AdHocAdvancedMappingsQueryCalciteTest : AdHocAdvancedMappingsQueryRelationalTestBase
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="fixture"></param>
        public AdHocAdvancedMappingsQueryCalciteTest(NonSharedFixture fixture) : base(fixture)
        {

        }
        /// <inheritdoc/>
        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}
