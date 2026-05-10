using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestModels.NullSemanticsModel;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class NullSemanticsQueryCalciteTest : NullSemanticsQueryTestBase<NullSemanticsQueryCalciteFixture>
{

    public NullSemanticsQueryCalciteTest(NullSemanticsQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper) : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
    }

    protected override NullSemanticsContext CreateContext(bool useRelationalNulls = false)
    {
        throw new System.NotImplementedException();
    }

}
