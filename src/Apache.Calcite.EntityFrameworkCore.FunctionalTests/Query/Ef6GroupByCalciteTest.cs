// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;
#nullable disable

public class Ef6GroupByCalciteTest : Ef6GroupByTestBase<Ef6GroupByCalciteTest.Ef6GroupByCalciteFixture>
{
    public Ef6GroupByCalciteTest(Ef6GroupByCalciteFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    public override async Task Average_Grouped_from_LINQ_101(bool async)
    {
        await base.Average_Grouped_from_LINQ_101(async);

        AssertSql(
            """
SELECT "p"."Category", ef_avg("p"."UnitPrice") AS "AveragePrice"
FROM "ProductForLinq" AS "p"
GROUP BY "p"."Category"
""");
    }

    public override async Task Max_Grouped_from_LINQ_101(bool async)
    {
        await base.Max_Grouped_from_LINQ_101(async);

        AssertSql(
            """
SELECT "p"."Category", ef_max("p"."UnitPrice") AS "MostExpensivePrice"
FROM "ProductForLinq" AS "p"
GROUP BY "p"."Category"
""");
    }

    public override async Task Min_Grouped_from_LINQ_101(bool async)
    {
        await base.Min_Grouped_from_LINQ_101(async);

        AssertSql(
            """
SELECT "p"."Category", ef_min("p"."UnitPrice") AS "CheapestPrice"
FROM "ProductForLinq" AS "p"
GROUP BY "p"."Category"
""");
    }

    public override async Task Whats_new_2021_sample_3(bool async)
        => await base.Whats_new_2021_sample_3(async);

    public override async Task Whats_new_2021_sample_5(bool async)
        => await base.Whats_new_2021_sample_5(async);

    public override async Task Whats_new_2021_sample_6(bool async)
        => await base.Whats_new_2021_sample_6(async);

    private void AssertSql(params string[] expected)
        => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

    public class Ef6GroupByCalciteFixture : Ef6GroupByFixtureBase, ITestSqlLoggerFactory
    {
        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

