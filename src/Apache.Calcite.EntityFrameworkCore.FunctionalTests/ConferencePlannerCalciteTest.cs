// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

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

