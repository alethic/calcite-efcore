using Apache.Calcite.EntityFrameworkCore.Diagnostics;
using Apache.Calcite.EntityFrameworkCore.Diagnostics.Internal;
using Apache.Calcite.EntityFrameworkCore.Internal;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Apache.Calcite.EntityFrameworkCore.Properties
{

    public static class CalciteResources
    {

        /// <summary>
        ///     The entity type '{entityType}' has composite key '{key}' which is configured to use generated values. SQLite does not support generated values on composite keys.
        /// </summary>
        public static EventDefinition<string> LogUnexpectedConnectionType(IDiagnosticsLogger logger)
        {
            var definition = ((CalciteLoggingDefinitions)logger.Definitions).LogUnexpectedConnectionType;
            if (definition == null)
            {
                definition = NonCapturingLazyInitializer.EnsureInitialized(
                    ref ((CalciteLoggingDefinitions)logger.Definitions).LogUnexpectedConnectionType,
                    logger,
                    static logger => new EventDefinition<string>(
                        logger.Options,
                        CalciteEventId.UnexpectedConnectionTypeWarning,
                        LogLevel.Warning,
                        "CalciteEventId.UnexpectedConnectionTypeWarning",
                        level => LoggerMessage.Define<string>(
                            level,
                            CalciteEventId.UnexpectedConnectionTypeWarning,
                            CalciteStrings.LogUnexpectedConnectionType)));
            }

            return (EventDefinition<string>)definition;
        }

    }

}
