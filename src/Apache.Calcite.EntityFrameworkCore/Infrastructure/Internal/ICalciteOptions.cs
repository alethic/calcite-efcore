using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal
{

    public interface ICalciteOptions : ISingletonOptions
    {

        string? ConnectionString { get; }

    }

}
