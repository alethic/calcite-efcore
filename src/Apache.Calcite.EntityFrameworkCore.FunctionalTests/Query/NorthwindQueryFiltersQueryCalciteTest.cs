using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class NorthwindQueryFiltersQueryCalciteTest : NorthwindQueryFiltersQueryTestBase<NorthwindQueryCalciteFixture<NorthwindQueryFiltersCustomizer>>
{

    public NorthwindQueryFiltersQueryCalciteTest(NorthwindQueryCalciteFixture<NorthwindQueryFiltersCustomizer> fixture, ITestOutputHelper testOutputHelper) :
        base(fixture)
    {
        fixture.TestSqlLoggerFactory.Clear();
        fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

}

