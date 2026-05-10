using System.Linq;
using System.Text;

using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Update;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Update;

public class StoreValueGenerationCalciteFixture : StoreValueGenerationFixtureBase
{
    private string? _cleanDataSql;

    protected override ITestStoreFactory TestStoreFactory
        => CalciteTestStoreFactory.Instance;

    public override void CleanData()
    {
        using var context = CreateContext();
        context.Database.ExecuteSqlRaw(_cleanDataSql ??= GenerateCleanDataSql());
    }

    private string GenerateCleanDataSql()
    {
        var context = CreateContext();
        var builder = new StringBuilder();

        foreach (var table in context.Model.GetEntityTypes().SelectMany(e => e.GetTableMappings().Select(m => m.Table.Name)))
        {
            builder.AppendLine($"DELETE FROM {table};");
        }

        return builder.ToString();
    }
}

