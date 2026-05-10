using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class ModelBuilding101CalciteTest : ModelBuilding101RelationalTestBase
{

    protected override DbContextOptionsBuilder ConfigureContext(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseCalcite();

}

