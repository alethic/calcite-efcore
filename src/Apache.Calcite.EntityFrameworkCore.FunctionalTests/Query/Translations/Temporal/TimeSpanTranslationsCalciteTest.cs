using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query.Translations.Temporal;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
using Xunit.Abstractions;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Translations.Temporal;

public class TimeSpanTranslationsCalciteTest : TimeSpanTranslationsTestBase<BasicTypesQueryCalciteFixture>
{
    public TimeSpanTranslationsCalciteTest(BasicTypesQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    // Translate TimeSpan members, #18844
    public override async Task Hours()
    {
        await AssertTranslationFailed(() => base.Hours());

        AssertSql();
    }

    // Translate TimeSpan members, #18844
    public override async Task Minutes()
    {
        await AssertTranslationFailed(() => base.Minutes());

        AssertSql();
    }

    public override async Task Seconds()
    {
        await AssertTranslationFailed(() => base.Seconds());

        AssertSql();
    }

    // Translate TimeSpan members, #18844
    public override async Task Milliseconds()
    {
        await AssertTranslationFailed(() => base.Milliseconds());

        AssertSql();
    }

    // Translate TimeSpan members, #18844
    public override async Task Microseconds()
    {
        await AssertTranslationFailed(() => base.Microseconds());

        AssertSql();
    }

    // Translate TimeSpan members, #18844
    public override async Task Nanoseconds()
    {
        await AssertTranslationFailed(() => base.Nanoseconds());

        AssertSql();
    }

    [ConditionalFact]
    public virtual void Check_all_tests_overridden()
        => TestHelpers.AssertAllMethodsOverridden(GetType());

    private void AssertSql(params string[] expected)
        => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
}

