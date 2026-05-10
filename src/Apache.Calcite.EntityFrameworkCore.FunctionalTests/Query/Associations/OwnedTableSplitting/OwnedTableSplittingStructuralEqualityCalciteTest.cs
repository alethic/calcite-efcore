using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query.Associations.OwnedTableSplitting;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedTableSplitting;

public class OwnedTableSplittingStructuralEqualityCalciteTest(OwnedTableSplittingCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
    OwnedTableSplittingStructuralEqualityRelationalTestBase<OwnedTableSplittingCalciteFixture>(fixture, testOutputHelper)
{



}

