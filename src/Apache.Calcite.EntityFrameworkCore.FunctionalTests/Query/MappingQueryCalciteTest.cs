using System;

using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class MappingQueryCalciteTest(MappingQueryCalciteTest.MappingQueryCalciteFixture fixture)
    : MappingQueryTestBase<MappingQueryCalciteTest.MappingQueryCalciteFixture>(fixture)
{
    public override void All_customers()
    {
        base.All_customers();

        Assert.Contains(
            @"SELECT ""c"".""CustomerID"", ""c"".""CompanyName""" + EOL + @"FROM ""Customers"" AS ""c""",
            Sql);
    }

    public override void All_employees()
    {
        base.All_employees();

        Assert.Contains(
            @"SELECT ""e"".""EmployeeID"", ""e"".""City""" + EOL + @"FROM ""Employees"" AS ""e""",
            Sql);
    }

    public override void All_orders()
    {
        base.All_orders();

        Assert.Contains(
            @"SELECT ""o"".""OrderID"", ""o"".""ShipVia""" + EOL + @"FROM ""Orders"" AS ""o""",
            Sql);
    }

    public override void Project_nullable_enum()
    {
        base.Project_nullable_enum();

        Assert.Contains(
            @"SELECT ""o"".""ShipVia""" + EOL + @"FROM ""Orders"" AS ""o""",
            Sql);
    }

    private static readonly string EOL = Environment.NewLine;

    private string Sql
        => Fixture.TestSqlLoggerFactory.Sql;

    public class MappingQueryCalciteFixture : MappingQueryFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override string DatabaseSchema { get; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<MappedCustomer>(e =>
            {
                e.Property(c => c.CompanyName2).Metadata.SetColumnName("CompanyName");
                e.Metadata.SetTableName("Customers");
            });
        }
    }
}

