// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.ComplexJson;
public class ComplexJsonMiscellaneousCalciteTest(ComplexJsonCalciteFixture fixture, ITestOutputHelper testOutputHelper)
    : ComplexJsonMiscellaneousRelationalTestBase<ComplexJsonCalciteFixture>(fixture, testOutputHelper)
{
    // TODO: #34627
    public override Task FromSql_on_root()
        => Assert.ThrowsAnyAsync<Exception>(base.FromSql_on_root);
}

