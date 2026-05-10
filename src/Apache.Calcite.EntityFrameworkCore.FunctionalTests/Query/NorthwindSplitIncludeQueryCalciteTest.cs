using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class NorthwindSplitIncludeQueryCalciteTest(NorthwindQueryCalciteFixture<NoopModelCustomizer> fixture) :
    NorthwindSplitIncludeQueryTestBase<NorthwindQueryCalciteFixture<NoopModelCustomizer>>(fixture)
{



}

