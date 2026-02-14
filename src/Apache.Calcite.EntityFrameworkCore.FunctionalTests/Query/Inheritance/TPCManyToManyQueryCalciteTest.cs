using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class TPCManyToManyQueryCalciteTest : TPCManyToManyQueryRelationalTestBase<TPCManyToManyQueryCalciteFixture>
{

    public TPCManyToManyQueryCalciteTest(TPCManyToManyQueryCalciteFixture fixture) : base(fixture)
    {

    }

}
