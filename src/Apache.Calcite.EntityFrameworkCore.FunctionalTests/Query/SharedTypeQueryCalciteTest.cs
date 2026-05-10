using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class SharedTypeQueryCalciteTest : SharedTypeQueryRelationalTestBase
    {

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

    }

}

