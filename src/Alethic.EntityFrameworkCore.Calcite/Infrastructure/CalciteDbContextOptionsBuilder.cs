using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Infrastructure
{

    /// <inheritdoc />
    public class CalciteDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<CalciteDbContextOptionsBuilder, CalciteOptionsExtension>
    {


        /// <inheritdoc />
        public CalciteDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) :
            base(optionsBuilder)
        {

        }

    }

}
