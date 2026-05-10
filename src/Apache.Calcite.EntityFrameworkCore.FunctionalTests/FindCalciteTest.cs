using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ModelBuilding;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public abstract class FindCalciteTest(FindCalciteTest.FindCalciteFixture fixture) : FindTestBase<FindCalciteTest.FindCalciteFixture>(fixture)
{
    public class FindCalciteTestSet(FindCalciteFixture fixture) : FindCalciteTest(fixture)
    {
        protected override TestFinder Finder { get; } = new FindViaSetFinder();
    }

    public class FindCalciteTestContext(FindCalciteFixture fixture) : FindCalciteTest(fixture)
    {
        protected override TestFinder Finder { get; } = new FindViaContextFinder();
    }

    public class FindCalciteTestNonGeneric(FindCalciteFixture fixture) : FindCalciteTest(fixture)
    {
        protected override TestFinder Finder { get; } = new FindViaNonGenericContextFinder();
    }

    public class FindCalciteFixture : FindFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<IntKey>(b =>
            {
                // This configuration for Calcite prevents attempts to use the default composite key config, which doesn't work
                // on Calcite. See #26708
                b.OwnsOne(
                    e => e.OwnedReference, b =>
                    {
                        b.OwnsOne(e => e.NestedOwned);
                        b.OwnsMany(e => e.NestedOwnedCollection).ToTable("NestedOwnedCollection").HasKey(e => e.Prop);
                    });

                b.OwnsMany(
                    e => e.OwnedCollection, b =>
                    {
                        b.ToTable("OwnedCollection").HasKey(e => e.Prop);
                        b.OwnsOne(e => e.NestedOwned);
                        b.OwnsMany(e => e.NestedOwnedCollection).ToTable("OwnedNestedOwnedCollection").HasKey(e => e.Prop);
                    });
            });
        }
    }
}

