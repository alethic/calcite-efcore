using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class QueryNoClientEvalCalciteFixture : NorthwindQueryCalciteFixture<NoopModelCustomizer>
    {

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

    }

}

