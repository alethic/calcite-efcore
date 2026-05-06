using System.Diagnostics.CodeAnalysis;

using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using com.sun.tools.@internal.ws.processor.model;

using java.util;

using jdk.@internal.dynalink.beans;

using Microsoft.EntityFrameworkCore.Metadata;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

    public static class CalciteModelExtensions
    {

        /// <summary>
        /// Gets the name of the sequence to use for generating values for properties of entity types in this model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string? GetEntitySequenceName(this IReadOnlyModel model)
        {
            return (string?)model[CalciteAnnotationNames.EntitySequenceName] ;
        }

        /// <summary>
        /// Gets the configuration source for the name of the sequence to use for generating values for properties of entity types in this model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ConfigurationSource? GetEntitySequenceNameConfigurationSource(this IConventionModel model)
        {
            return model.FindAnnotation(CalciteAnnotationNames.EntitySequenceName)?.GetConfigurationSource();
        }

        /// <summary>
        /// Sets the entity sequence name annotation for the specified model.
        /// </summary>
        /// <remarks>This method adds or removes a model-level annotation that can be used to store a
        /// custom entity sequence name. If the specified name is null, the annotation is removed.</remarks>
        /// <param name="model">The model to which the entity sequence name annotation will be applied. Cannot be null.</param>
        /// <param name="name">The entity sequence name to set, or null to remove the annotation.</param>
        public static void SetEntitySequenceName(this IMutableModel model, string? name)
        {
            model.SetOrRemoveAnnotation(CalciteAnnotationNames.EntitySequenceName, name);
        }

        /// <summary>
        /// Sets the entity sequence name annotation for the specified model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name"></param>
        /// <param name="fromDataAnnotation"></param>
        /// <returns></returns>
        [return: NotNullIfNotNull(nameof(name))]
        public static string? SetEntitySequenceName(this IConventionModel model, string? name, bool fromDataAnnotation = false)
        {
            return (string?)model.SetOrRemoveAnnotation(CalciteAnnotationNames.EntitySequenceName, name, fromDataAnnotation)?.Value;
        }

        /// <summary>
        /// Finds the entity sequence with the given name in the model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICalciteEntitySequence? FindEntitySequence(this IReadOnlyModel model, string name)
        {
            return CalciteEntitySequence.FindEntitySequence(model, name);
        }

        public static CalciteValueGenerationStrategy? GetValueGenerationStrategy(this IReadOnlyModel model)
        {
            return (CalciteValueGenerationStrategy?)model[CalciteAnnotationNames.ValueGenerationStrategy];
        }

        public static void SetValueGenerationStrategy(this IMutableModel model, CalciteValueGenerationStrategy? value)
        {
            model.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value);
        }

        public static CalciteValueGenerationStrategy? SetValueGenerationStrategy(this IConventionModel model, CalciteValueGenerationStrategy? value, bool fromDataAnnotation = false)
        {
            return (CalciteValueGenerationStrategy?)model.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation)?.Value;
        }

        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(this IConventionModel model)
        {
            return model.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();
        }

    }

}
