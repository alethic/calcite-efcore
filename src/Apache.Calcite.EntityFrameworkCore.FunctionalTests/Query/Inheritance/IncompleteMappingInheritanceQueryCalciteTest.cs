using Microsoft.EntityFrameworkCore.Query;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class IncompleteMappingInheritanceQueryCalciteTest : TPHInheritanceQueryTestBase<IncompleteMappingInheritanceQueryCalciteFixture>
{

    public IncompleteMappingInheritanceQueryCalciteTest(IncompleteMappingInheritanceQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
        base(fixture, testOutputHelper)
    {

    }

}
