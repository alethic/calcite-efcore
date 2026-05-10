using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit.Abstractions;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class FunkyDataQueryCalciteTest : FunkyDataQueryTestBase<FunkyDataQueryCalciteTest.FunkyDataQueryCalciteFixture>
{
    public FunkyDataQueryCalciteTest(FunkyDataQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    public override async Task String_Contains_and_StartsWith_with_same_parameter(bool async)
    {
        await base.String_Contains_and_StartsWith_with_same_parameter(async);

        AssertSql(
            """
@s='B' (Size = 1)
@s_startswith='B%' (Size = 2)

SELECT "f"."Id", "f"."FirstName", "f"."LastName", "f"."NullableBool"
FROM "FunkyCustomers" AS "f"
WHERE instr("f"."FirstName", @s) > 0 OR "f"."LastName" LIKE @s_startswith ESCAPE '\'
""");
    }

    private void AssertSql(params string[] expected)
        => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

    public class FunkyDataQueryCalciteFixture : FunkyDataQueryFixtureBase, ITestSqlLoggerFactory
    {
        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

