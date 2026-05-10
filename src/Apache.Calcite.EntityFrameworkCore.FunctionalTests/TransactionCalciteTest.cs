// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class TransactionCalciteTest(TransactionCalciteTest.TransactionCalciteFixture fixture)
    : TransactionTestBase<TransactionCalciteTest.TransactionCalciteFixture>(fixture)
{
    protected override bool SnapshotSupported
        => false;

    protected override DbContext CreateContextWithConnectionString()
    {
        var options = Fixture.AddOptions(
                new DbContextOptionsBuilder().UseCalcite(TestStore.ConnectionString)
                    .ConfigureWarnings(w => w.Log(RelationalEventId.AmbientTransactionWarning)))
            .UseInternalServiceProvider(Fixture.ServiceProvider);

        return new DbContext(options.Options);
    }

    public class TransactionCalciteFixture : TransactionFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public override async Task ReseedAsync()
        {
            using var context = CreateContext();
            context.Set<TransactionCustomer>().RemoveRange(await context.Set<TransactionCustomer>().ToListAsync());
            context.Set<TransactionOrder>().RemoveRange(await context.Set<TransactionOrder>().ToListAsync());
            await context.SaveChangesAsync();

            await SeedAsync(context);
        }

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder)
                .ConfigureWarnings(w => w.Log(RelationalEventId.AmbientTransactionWarning));
    }
}

