// ReSharper disable InconsistentNaming
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class StoreGeneratedFixupCalciteTest(StoreGeneratedFixupCalciteTest.StoreGeneratedFixupCalciteFixture fixture)
    : StoreGeneratedFixupRelationalTestBase<
        StoreGeneratedFixupCalciteTest.StoreGeneratedFixupCalciteFixture>(fixture)
{
    [ConditionalFact]
    public void Temp_values_can_be_made_permanent()
    {
        using var context = CreateContext();
        var entry = context.Add(new TestTemp());

        Assert.True(entry.Property(e => e.Id).IsTemporary);
        Assert.False(entry.Property(e => e.NotId).IsTemporary);

        var tempValue = entry.Property(e => e.Id).CurrentValue;

        entry.Property(e => e.Id).IsTemporary = false;

        context.SaveChanges();

        Assert.False(entry.Property(e => e.Id).IsTemporary);
        Assert.Equal(tempValue, entry.Property(e => e.Id).CurrentValue);
    }

    protected override bool EnforcesFKs
        => true;

    protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
        => facade.UseTransaction(transaction.GetDbTransaction());

    public class StoreGeneratedFixupCalciteFixture : StoreGeneratedFixupRelationalFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

