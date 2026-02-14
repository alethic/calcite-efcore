using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class TPCGearsOfWarQueryCalciteFixture : TPCGearsOfWarQueryRelationalFixture
{

    protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

}
