using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

#nullable disable

public class TPCManyToManyNoTrackingQueryCalciteTest(TPCManyToManyQueryCalciteFixture fixture) :
    TPCManyToManyNoTrackingQueryRelationalTestBase<TPCManyToManyQueryCalciteFixture>(fixture)
{

}
