using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class TransactionCalciteTest(TransactionCalciteTest.TransactionCalciteFixture fixture) :
    TransactionTestBase<TransactionCalciteTest.TransactionCalciteFixture>(fixture)
{
    protected override bool SnapshotSupported => throw new System.NotImplementedException();

    protected override DbContext CreateContextWithConnectionString()
    {
        throw new System.NotImplementedException();
    }

    public class TransactionCalciteFixture : TransactionFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

