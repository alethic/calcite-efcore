using Microsoft.EntityFrameworkCore.Query.Associations.ComplexTableSplitting;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.ComplexTableSplitting;

public class ComplexTableSplittingProjectionCalciteTest(ComplexTableSplittingCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
    ComplexTableSplittingProjectionRelationalTestBase<ComplexTableSplittingCalciteFixture>(fixture, testOutputHelper)
{



}
