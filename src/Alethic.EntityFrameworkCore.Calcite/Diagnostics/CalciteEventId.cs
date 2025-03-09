using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Alethic.EntityFrameworkCore.Calcite.Diagnostics
{

    public static class CalciteEventId
    {

        enum Id
        {

            UnexpectedConnectionTypeWarning = CoreEventId.ProviderBaseId + 100,

        }

        static readonly string InfraPrefix = DbLoggerCategory.Infrastructure.Name + ".";

        static EventId MakeInfraId(Id id) => new((int)id, InfraPrefix + id);

        public static readonly EventId UnexpectedConnectionTypeWarning = MakeInfraId(Id.UnexpectedConnectionTypeWarning);

    }

}
