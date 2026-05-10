using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class AdHocJsonQueryCalciteTest(NonSharedFixture fixture) : AdHocJsonQueryRelationalTestBase(fixture)
{

    protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    protected override Task Seed30028(DbContext ctx)
    {
        throw new System.NotImplementedException();
    }

    protected override Task Seed33046(DbContext ctx)
    {
        throw new System.NotImplementedException();
    }

    protected override Task SeedBadJsonProperties(ContextBadJsonProperties ctx)
    {
        throw new System.NotImplementedException();
    }

    protected override Task SeedJunkInJson(DbContext ctx)
    {
        throw new System.NotImplementedException();
    }

    protected override Task SeedNotICollection(DbContext ctx)
    {
        throw new System.NotImplementedException();
    }

    protected override Task SeedShadowProperties(DbContext ctx)
    {
        throw new System.NotImplementedException();
    }

    protected override Task SeedTrickyBuffering(DbContext ctx)
    {
        throw new System.NotImplementedException();
    }

}

