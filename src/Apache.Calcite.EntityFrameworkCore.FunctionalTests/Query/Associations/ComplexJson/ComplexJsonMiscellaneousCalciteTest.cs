using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query.Associations.ComplexJson;

using Xunit;
using Xunit.Abstractions;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.ComplexJson;

public class ComplexJsonMiscellaneousCalciteTest(ComplexJsonCalciteFixture fixture, ITestOutputHelper testOutputHelper)
    : ComplexJsonMiscellaneousRelationalTestBase<ComplexJsonCalciteFixture>(fixture, testOutputHelper)
{
    // TODO: #34627
    public override Task FromSql_on_root()
        => Assert.ThrowsAnyAsync<Exception>(base.FromSql_on_root);
}

