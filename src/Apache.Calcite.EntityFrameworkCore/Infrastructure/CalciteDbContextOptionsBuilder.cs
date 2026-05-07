using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Infrastructure
{

    /// <summary>
    /// Allows Calcite-specific configuration to be performed on a <see cref="DbContextOptionsBuilder"/>.
    /// Instances of this class are typically obtained from a call to
    /// <see cref="Extensions.CalciteDbContextOptionsBuilderExtensions.UseCalcite(DbContextOptionsBuilder, System.Action{CalciteDbContextOptionsBuilder}?)"/> and are not designed to be directly constructed in your application code.
    /// </summary>
    public class CalciteDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<CalciteDbContextOptionsBuilder, CalciteOptionsExtension>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CalciteDbContextOptionsBuilder"/> class.
        /// </summary>
        /// <param name="optionsBuilder">The core options builder being decorated.</param>
        public CalciteDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) :
            base(optionsBuilder)
        {

        }

    }

}
