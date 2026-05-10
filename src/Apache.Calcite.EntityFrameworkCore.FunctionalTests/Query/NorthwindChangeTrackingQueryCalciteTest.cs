// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;
#nullable disable

public class NorthwindChangeTrackingQueryCalciteTest(NorthwindQueryCalciteFixture<NoopModelCustomizer> fixture)
    : NorthwindChangeTrackingQueryTestBase<
        NorthwindQueryCalciteFixture<NoopModelCustomizer>>(fixture)
{
    protected override NorthwindContext CreateNoTrackingContext()
        => new NorthwindSqliteContext(
            new DbContextOptionsBuilder(Fixture.CreateOptions())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options);
}

