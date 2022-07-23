using System;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal
{

    /// <summary>
    /// Represents options for Calcite that can only be set at the <see cref="IServiceProvider"/> singleton level.
    /// </summary>
    public interface ICalciteSingletonOptions : ISingletonOptions
    {



    }

}
