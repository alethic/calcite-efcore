// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;
#nullable disable

public class ManyToManyNoTrackingQueryCalciteTest(ManyToManyQueryCalciteFixture fixture)
    : ManyToManyNoTrackingQueryRelationalTestBase<ManyToManyQueryCalciteFixture>(fixture)
{
    // Sqlite does not support Apply operations

    public override async Task Filtered_include_skip_navigation_order_by_skip_take_then_include_skip_navigation_where(bool async)
        => Assert.Equal(
            SqliteStrings.ApplyNotSupported,
            (await Assert.ThrowsAsync<InvalidOperationException>(()
                => base.Filtered_include_skip_navigation_order_by_skip_take_then_include_skip_navigation_where(async))).Message);

    public override async Task Filtered_include_skip_navigation_order_by_skip_take_then_include_skip_navigation_where_EF_Property(
        bool async)
        => Assert.Equal(
            SqliteStrings.ApplyNotSupported,
            (await Assert.ThrowsAsync<InvalidOperationException>(()
                => base.Filtered_include_skip_navigation_order_by_skip_take_then_include_skip_navigation_where_EF_Property(async)))
            .Message);
}

