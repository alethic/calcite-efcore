using Microsoft.EntityFrameworkCore.Query.Translations.Operators;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Translations.Operators;

public class ComparisonOperatorTranslationsCalciteTest : ComparisonOperatorTranslationsTestBase<BasicTypesQueryCalciteFixture>
{

    public ComparisonOperatorTranslationsCalciteTest(BasicTypesQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

}

