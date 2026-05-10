using Microsoft.EntityFrameworkCore.Query;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class OptionalDependentQueryCalciteTest : OptionalDependentQueryTestBase<OptionalDependentQueryCalciteFixture>
{

    public OptionalDependentQueryCalciteTest(OptionalDependentQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper) : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

}

