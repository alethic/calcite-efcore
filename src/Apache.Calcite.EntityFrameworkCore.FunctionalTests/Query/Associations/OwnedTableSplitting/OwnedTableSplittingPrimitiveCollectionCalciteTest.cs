using Microsoft.EntityFrameworkCore.Query.Associations.OwnedTableSplitting;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedTableSplitting;

public class OwnedTableSplittingPrimitiveCollectionCalciteTest(OwnedTableSplittingCalciteFixture fixture, ITestOutputHelper testOutputHelper)    : 
    OwnedTableSplittingPrimitiveCollectionRelationalTestBase<OwnedTableSplittingCalciteFixture>(fixture, testOutputHelper);

