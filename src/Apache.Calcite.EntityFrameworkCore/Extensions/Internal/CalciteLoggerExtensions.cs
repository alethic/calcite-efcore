using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using Apache.Calcite.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Apache.Calcite.EntityFrameworkCore.Properties;

namespace Apache.Calcite.EntityFrameworkCore.Extensions.Internal
{

    public static class CalciteLoggerExtensions
    {

        public static void UnexpectedConnectionTypeWarning(this IDiagnosticsLogger<DbLoggerCategory.Infrastructure> diagnostics, Type connectionType)
        {
            var definition = CalciteResources.LogUnexpectedConnectionType(diagnostics);

            if (diagnostics.ShouldLog(definition))
            {
                definition.Log(diagnostics, connectionType.ShortDisplayName());
            }

            if (diagnostics.NeedsEventData(definition, out var diagnosticSourceEnabled, out var simpleLogEnabled))
            {
                var eventData = new UnexpectedConnectionTypeEventData(definition, UnexpectedConnectionTypeWarning, connectionType);
                diagnostics.DispatchEventData(definition, eventData, diagnosticSourceEnabled, simpleLogEnabled);
            }
        }

        static string UnexpectedConnectionTypeWarning(EventDefinitionBase definition, EventData payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (UnexpectedConnectionTypeEventData)payload;
            return d.GenerateMessage(p.ConnectionType.ShortDisplayName());
        }

    }

}
