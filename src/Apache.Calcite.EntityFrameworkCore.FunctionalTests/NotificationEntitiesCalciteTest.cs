// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class NotificationEntitiesCalciteTest(NotificationEntitiesCalciteTest.NotificationEntitiesCalciteFixture fixture)
    : NotificationEntitiesTestBase<
        NotificationEntitiesCalciteTest.NotificationEntitiesCalciteFixture>(fixture)
{
    public class NotificationEntitiesCalciteFixture : NotificationEntitiesFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

