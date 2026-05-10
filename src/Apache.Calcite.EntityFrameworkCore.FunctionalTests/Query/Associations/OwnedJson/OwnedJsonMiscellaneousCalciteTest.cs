using Microsoft.EntityFrameworkCore.Query.Associations.OwnedJson;

using Xunit.Abstractions;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.OwnedJson;

public class OwnedJsonMiscellaneousCalciteTest(OwnedJsonCalciteFixture fixture, ITestOutputHelper testOutputHelper)
    : OwnedJsonMiscellaneousRelationalTestBase<OwnedJsonCalciteFixture>(fixture, testOutputHelper);

