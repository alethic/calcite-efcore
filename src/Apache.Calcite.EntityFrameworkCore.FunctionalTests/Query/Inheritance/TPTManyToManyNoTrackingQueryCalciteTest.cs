using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class TPTManyToManyNoTrackingQueryCalciteTest : TPTManyToManyNoTrackingQueryRelationalTestBase<TPTManyToManyQueryCalciteFixture>
{

    public TPTManyToManyNoTrackingQueryCalciteTest(TPTManyToManyQueryCalciteFixture fixture) : base(fixture)
    {

    }

}
