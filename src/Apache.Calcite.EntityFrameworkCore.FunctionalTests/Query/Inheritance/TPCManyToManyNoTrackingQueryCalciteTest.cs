using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class TPCManyToManyNoTrackingQueryCalciteTest(TPCManyToManyQueryCalciteFixture fixture) :
    TPCManyToManyNoTrackingQueryRelationalTestBase<TPCManyToManyQueryCalciteFixture>(fixture)
{

}
