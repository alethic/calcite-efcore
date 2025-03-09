using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using Alethic.EntityFrameworkCore.Calcite.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Alethic.EntityFrameworkCore.Calcite.Properties;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions.Internal
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
