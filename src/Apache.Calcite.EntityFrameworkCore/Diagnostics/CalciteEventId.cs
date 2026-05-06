using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Apache.Calcite.EntityFrameworkCore.Diagnostics
{

    /// <summary>
    /// Event IDs for Calcite events that correspond to messages logged to an <see cref="ILogger" /> and events sent to a <see cref="DiagnosticSource" />.
    /// </summary>
    public static class CalciteEventId
    {

        enum Id
        {

            // Core events
            UnexpectedConnectionTypeWarning = CoreEventId.ProviderBaseId,

            // Transaction events
            TransactionIgnoredWarning = CoreEventId.ProviderBaseId + 100,

        }

        static readonly string InfrastructurePrefix = DbLoggerCategory.Infrastructure.Name + ".";
        static readonly string TransactionPrefix = DbLoggerCategory.Database.Transaction.Name + ".";

        static EventId MakeCoreId(Id id) => new((int)id, InfrastructurePrefix + id);

        public static readonly EventId UnexpectedConnectionTypeWarning = MakeCoreId(Id.UnexpectedConnectionTypeWarning);

        static EventId MakeTransactionId(Id id) => new((int)id, TransactionPrefix + id);

        /// <summary>
        /// A transaction operation was requested, but ignored because Calcite does not support transactions.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This event is in the <see cref="DbLoggerCategory.Database.Transaction" /> category.
        ///     </para>
        ///     <para>
        ///         This event uses the <see cref="EventData" /> payload when used with a <see cref="DiagnosticSource" />.
        ///     </para>
        /// </remarks>
        public static readonly EventId TransactionIgnoredWarning = MakeTransactionId(Id.TransactionIgnoredWarning);

    }

}
