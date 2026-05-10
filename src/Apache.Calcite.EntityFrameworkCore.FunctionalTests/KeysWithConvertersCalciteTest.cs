// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class KeysWithConvertersCalciteTest(KeysWithConvertersCalciteTest.KeysWithConvertersCalciteFixture fixture)
    : KeysWithConvertersTestBase<KeysWithConvertersCalciteTest.KeysWithConvertersCalciteFixture>(fixture)
{
    public class KeysWithConvertersCalciteFixture : KeysWithConvertersFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder)
                .UseCalcite(b => b.MinBatchSize(1));
    }
}

