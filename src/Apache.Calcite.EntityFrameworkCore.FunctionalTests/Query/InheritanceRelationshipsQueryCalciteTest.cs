using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class InheritanceRelationshipsQueryCalciteTest(InheritanceRelationshipsQueryCalciteTest.InheritanceRelationshipsQueryCalciteFixture fixture) :
    InheritanceRelationshipsQueryRelationalTestBase<InheritanceRelationshipsQueryCalciteTest.InheritanceRelationshipsQueryCalciteFixture>(fixture)
{

    public class InheritanceRelationshipsQueryCalciteFixture : InheritanceRelationshipsQueryRelationalFixture
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

