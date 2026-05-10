using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    /// <inheritdoc/>
    public class NorthwindWhereQueryCalciteTest : NorthwindWhereQueryRelationalTestBase<NorthwindQueryCalciteFixture<NoopModelCustomizer>>
    {

        public NorthwindWhereQueryCalciteTest(NorthwindQueryCalciteFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper) :
            base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        /// <inheritdoc/>
        public override async Task Where_ternary_boolean_condition_negated(bool async)
        {
            await base.Where_ternary_boolean_condition_negated(async);

            AssertSql(
                """
SELECT "p"."ProductID", "p"."Discontinued", "p"."ProductName", "p"."SupplierID", "p"."UnitPrice", "p"."UnitsInStock"
FROM "Products" AS "p"
WHERE CASE
    WHEN "p"."UnitsInStock" >= 20 THEN TRUE
    ELSE FALSE
END
""");
        }

        void AssertSql(params string[] expected) => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

    }

}
