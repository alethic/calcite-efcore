using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Migrations;

public class MigrationsCalciteTest : MigrationsTestBase<MigrationsCalciteTest.MigrationsCalciteFixture>
{

    public MigrationsCalciteTest(MigrationsCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
        base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    protected override string NonDefaultCollation => throw new NotImplementedException();

    public override Task Add_required_primitive_collection_with_custom_default_value_sql_to_existing_table()
    {
        throw new NotImplementedException();
    }

    public override Task Add_required_primitve_collection_with_custom_default_value_sql_to_existing_table()
    {
        throw new NotImplementedException();
    }

    public class MigrationsCalciteFixture : MigrationsFixtureBase
    {

        public override RelationalTestHelpers TestHelpers => throw new NotImplementedException();

        protected override string StoreName => throw new NotImplementedException();

        protected override ITestStoreFactory TestStoreFactory => throw new NotImplementedException();

    }

}
