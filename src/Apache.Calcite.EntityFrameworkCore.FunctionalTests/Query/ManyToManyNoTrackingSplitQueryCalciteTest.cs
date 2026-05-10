using Microsoft.EntityFrameworkCore.Query;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class ManyToManyNoTrackingSplitQueryCalciteTest(ManyToManySplitQueryCalciteFixture fixture)
    : ManyToManyNoTrackingQueryRelationalTestBase<ManyToManySplitQueryCalciteFixture>(fixture)
{
    // Calcite does not support Apply operations
}

