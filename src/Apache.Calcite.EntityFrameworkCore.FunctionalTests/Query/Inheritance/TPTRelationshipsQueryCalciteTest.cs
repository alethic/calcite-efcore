using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Inheritance;

public class TPTRelationshipsQueryCalciteTest : TPTRelationshipsQueryTestBase<TPTRelationshipsQueryCalciteTest.TPTRelationshipsQuerySqliteFixture>
{

    public TPTRelationshipsQueryCalciteTest(TPTRelationshipsQuerySqliteFixture fixture) :
        base(fixture)
    {

    }

    public class TPTRelationshipsQuerySqliteFixture : TPTRelationshipsQueryRelationalFixture
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}
