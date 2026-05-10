using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

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

