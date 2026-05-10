using Microsoft.EntityFrameworkCore.Query.Associations.OwnedTableSplitting;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedTableSplitting;

public class OwnedTableSplittingMiscellaneousCalciteTest(OwnedTableSplittingCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
    OwnedTableSplittingMiscellaneousRelationalTestBase<OwnedTableSplittingCalciteFixture>(fixture, testOutputHelper);

