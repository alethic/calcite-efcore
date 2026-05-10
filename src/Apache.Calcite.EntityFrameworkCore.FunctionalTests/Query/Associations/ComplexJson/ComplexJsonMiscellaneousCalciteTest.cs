using Microsoft.EntityFrameworkCore.Query.Associations.ComplexJson;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.ComplexJson;

public class ComplexJsonMiscellaneousCalciteTest(ComplexJsonCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
    ComplexJsonMiscellaneousRelationalTestBase<ComplexJsonCalciteFixture>(fixture, testOutputHelper)
{



}

