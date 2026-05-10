using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query;

using Xunit.Abstractions;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class NorthwindQueryFiltersQueryCalciteTest : NorthwindQueryFiltersQueryTestBase<
    NorthwindQueryCalciteFixture<NorthwindQueryFiltersCustomizer>>
{
    public NorthwindQueryFiltersQueryCalciteTest(
        NorthwindQueryCalciteFixture<NorthwindQueryFiltersCustomizer> fixture,
        ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        fixture.TestSqlLoggerFactory.Clear();
        fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    public override async Task Count_query(bool async)
    {
        await base.Count_query(async);

        AssertSql(
            """
@ef_filter__TenantPrefix_startswith='B%' (Size = 2)

SELECT COUNT(*)
FROM "Customers" AS "c"
WHERE "c"."CompanyName" LIKE @ef_filter__TenantPrefix_startswith ESCAPE '\'
""");
    }

    private void AssertSql(params string[] expected)
        => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
}

