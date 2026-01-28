using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Internal
{

    public class CalciteOptions : ICalciteOptions
    {

        public virtual void Initialize(IDbContextOptions options)
        {
            var calciteJdbcOptions = options.FindExtension<CalciteOptionsExtension>() ?? new CalciteOptionsExtension();
            ConnectionString = calciteJdbcOptions.Connection?.ConnectionString ?? calciteJdbcOptions.ConnectionString!;
        }

        public virtual void Validate(IDbContextOptions options)
        {

        }

        public virtual string? ConnectionString { get; private set; }

    }

}
