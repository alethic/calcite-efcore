using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class NotificationEntitiesCalciteTest(NotificationEntitiesCalciteTest.NotificationEntitiesCalciteFixture fixture) :
    NotificationEntitiesTestBase<NotificationEntitiesCalciteTest.NotificationEntitiesCalciteFixture>(fixture)
{

    public class NotificationEntitiesCalciteFixture : NotificationEntitiesFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}
