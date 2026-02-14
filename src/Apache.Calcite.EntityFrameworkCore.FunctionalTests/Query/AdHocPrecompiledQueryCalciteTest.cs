using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class AdHocPrecompiledQueryCalciteTest : AdHocPrecompiledQueryRelationalTestBase
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="testOutputHelper"></param>
        public AdHocPrecompiledQueryCalciteTest(NonSharedFixture fixture, ITestOutputHelper testOutputHelper) : base(fixture, testOutputHelper)
        {

        }

        /// <inheritdoc/>
        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

        /// <inheritdoc/>
        protected override PrecompiledQueryTestHelpers PrecompiledQueryTestHelpers => CalcitePrecompiledQueryTestHelpers.Instance;

    }

}
