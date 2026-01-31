using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestModels.Northwind
{

    public class NorthwindCalciteContext(DbContextOptions options) : NorthwindRelationalContext(options);

}
