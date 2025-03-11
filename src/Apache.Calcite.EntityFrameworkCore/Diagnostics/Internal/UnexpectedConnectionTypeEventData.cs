using System;

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Apache.Calcite.EntityFrameworkCore.Diagnostics.Internal
{

    public class UnexpectedConnectionTypeEventData : EventData
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="eventDefinition">The event definition.</param>
        /// <param name="messageGenerator">A delegate that generates a log message for this event.</param>
        /// <param name="connectionType">The connection type.</param>
        public UnexpectedConnectionTypeEventData(EventDefinitionBase eventDefinition, Func<EventDefinitionBase, EventData, string> messageGenerator, Type connectionType) :
            base(eventDefinition, messageGenerator)
        {
            ConnectionType = connectionType;
        }

        /// <summary>
        /// The connection type.
        /// </summary>
        public virtual Type ConnectionType { get; }

    }

}
