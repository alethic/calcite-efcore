using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class OwnedEntityQueryCalciteTest : OwnedEntityQueryRelationalTestBase
    {

        public OwnedEntityQueryCalciteTest(NonSharedFixture fixture) : base(fixture)
        {

        }

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

