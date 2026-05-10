using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class ConferencePlannerCalciteTest(ConferencePlannerCalciteTest.ConferencePlannerCalciteFixture fixture)
    : ConferencePlannerTestBase<ConferencePlannerCalciteTest.ConferencePlannerCalciteFixture>(fixture)
{
    protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
        => facade.UseTransaction(transaction.GetDbTransaction());

    public class ConferencePlannerCalciteFixture : ConferencePlannerFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

