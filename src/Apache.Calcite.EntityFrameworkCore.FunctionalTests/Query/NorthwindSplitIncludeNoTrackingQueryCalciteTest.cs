using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class NorthwindSplitIncludeNoTrackingQueryCalciteTest(NorthwindQueryCalciteFixture<NoopModelCustomizer> fixture) :
    NorthwindSplitIncludeNoTrackingQueryTestBase<NorthwindQueryCalciteFixture<NoopModelCustomizer>>(fixture)
{



}
