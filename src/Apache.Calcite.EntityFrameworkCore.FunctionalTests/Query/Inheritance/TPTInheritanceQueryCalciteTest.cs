using Microsoft.EntityFrameworkCore.Query;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class TPTInheritanceQueryCalciteTest :
    TPTInheritanceQueryTestBase<TPTInheritanceQueryCalciteFixture>
{

    public TPTInheritanceQueryCalciteTest(TPTInheritanceQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
        base(fixture, testOutputHelper)
    {

    }

}
