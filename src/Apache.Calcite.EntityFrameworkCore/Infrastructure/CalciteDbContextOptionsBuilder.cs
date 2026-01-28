using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Infrastructure
{

    public class CalciteDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<CalciteDbContextOptionsBuilder, CalciteOptionsExtension>
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public CalciteDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) :
            base(optionsBuilder)
        {

        }

    }

}
