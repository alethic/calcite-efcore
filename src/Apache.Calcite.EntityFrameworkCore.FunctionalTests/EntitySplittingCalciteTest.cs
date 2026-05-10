using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests
{

    public class EntitySplittingCalciteTest : EntitySplittingTestBase
    {

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

    }

}

