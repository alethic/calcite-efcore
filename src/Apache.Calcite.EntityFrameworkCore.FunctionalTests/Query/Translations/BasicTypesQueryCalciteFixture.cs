using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query.Translations;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Translations;

public class BasicTypesQueryCalciteFixture : BasicTypesQueryFixtureBase, ITestSqlLoggerFactory
{

    protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

}

