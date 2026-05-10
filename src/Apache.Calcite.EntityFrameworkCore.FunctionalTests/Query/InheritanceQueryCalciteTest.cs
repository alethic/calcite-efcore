// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;
#nullable disable

public class InheritanceQueryCalciteTest(TPHInheritanceQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper)
    : TPHInheritanceQueryTestBase<TPHInheritanceQueryCalciteFixture>(fixture, testOutputHelper)
{
    protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
        => facade.UseTransaction(transaction.GetDbTransaction());
}

