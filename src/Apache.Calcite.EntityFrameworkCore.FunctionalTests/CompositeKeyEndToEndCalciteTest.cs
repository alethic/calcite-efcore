// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class CompositeKeyEndToEndCalciteTest(CompositeKeyEndToEndCalciteTest.CompositeKeyEndToEndCalciteFixture fixture)
    : CompositeKeyEndToEndTestBase<
        CompositeKeyEndToEndCalciteTest.CompositeKeyEndToEndCalciteFixture>(fixture)
{
    public override Task Can_use_generated_values_in_composite_key_end_to_end()
        // Not supported on Sqlite
        => Task.CompletedTask;

    public class CompositeKeyEndToEndCalciteFixture : CompositeKeyEndToEndFixtureBase
    {
        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder.ConfigureWarnings(b => b.Ignore(SqliteEventId.CompositeKeyWithValueGeneration)));

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

