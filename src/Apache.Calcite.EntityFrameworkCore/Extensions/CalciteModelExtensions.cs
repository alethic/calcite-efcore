using System.Diagnostics.CodeAnalysis;

using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using com.sun.tools.@internal.ws.processor.model;

using java.util;

using jdk.@internal.dynalink.beans;

using Microsoft.EntityFrameworkCore.Metadata;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

    /// <summary>
    /// Calcite-specific extension methods for <see cref="IReadOnlyModel"/>, providing access to model-level
    /// sequence configuration, default sequence backing entity metadata, and value generation strategy.
    /// </summary>
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

        /// <summary>
        /// Gets the model-level Calcite value generation strategy, if one has been configured.
        /// </summary>
        /// <param name="model">The model to inspect.</param>
        /// <returns>The strategy, or <see langword="null"/> if none has been configured.</returns>
        public static CalciteValueGenerationStrategy? GetValueGenerationStrategy(this IReadOnlyModel model)
        {
            return (CalciteValueGenerationStrategy?)model[CalciteAnnotationNames.ValueGenerationStrategy];
        }

        /// <summary>
        /// Sets the model-level Calcite value generation strategy.
        /// </summary>
        /// <param name="model">The model to update.</param>
        /// <param name="value">The strategy to apply, or <see langword="null"/> to clear.</param>
        public static void SetValueGenerationStrategy(this IMutableModel model, CalciteValueGenerationStrategy? value)
        {
            model.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value);
        }

        /// <summary>
        /// Sets the model-level Calcite value generation strategy.
        /// </summary>
        /// <param name="model">The model to update.</param>
        /// <param name="value">The strategy to apply, or <see langword="null"/> to clear.</param>
        /// <param name="fromDataAnnotation">Whether the configuration originates from a data annotation.</param>
        /// <returns>The applied strategy, or <see langword="null"/> if it could not be set.</returns>
        public static CalciteValueGenerationStrategy? SetValueGenerationStrategy(this IConventionModel model, CalciteValueGenerationStrategy? value, bool fromDataAnnotation = false)
        {
            return (CalciteValueGenerationStrategy?)model.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation)?.Value;
        }

        /// <summary>
        /// Gets the configuration source for the model-level value generation strategy annotation, if any.
        /// </summary>
        /// <param name="model">The model to inspect.</param>
        /// <returns>The configuration source, or <see langword="null"/> if not configured.</returns>
        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(this IConventionModel model)
        {
            return model.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();
        }

        /// <summary>
        /// Gets the CLR type of the entity that backs default per-entity HiLo sequences for this model, if configured.
        /// </summary>
        public static System.Type? GetDefaultEntitySequenceEntityType(this IReadOnlyModel model)
        {
            return (System.Type?)model[CalciteAnnotationNames.DefaultEntitySequenceEntityType];
        }

        /// <summary>
        /// Sets the CLR type of the entity that backs default per-entity HiLo sequences for this model.
        /// </summary>
        public static void SetDefaultEntitySequenceEntityType(this IMutableModel model, System.Type? type)
        {
            model.SetOrRemoveAnnotation(CalciteAnnotationNames.DefaultEntitySequenceEntityType, type);
        }

        /// <summary>
        /// Gets the name of the property on the default sequence backing entity that identifies a sequence row.
        /// </summary>
        public static string? GetDefaultEntitySequenceNameProperty(this IReadOnlyModel model)
        {
            return (string?)model[CalciteAnnotationNames.DefaultEntitySequenceNameProperty];
        }

        /// <summary>
        /// Sets the name of the property on the default sequence backing entity that identifies a sequence row.
        /// </summary>
        public static void SetDefaultEntitySequenceNameProperty(this IMutableModel model, string? name)
        {
            model.SetOrRemoveAnnotation(CalciteAnnotationNames.DefaultEntitySequenceNameProperty, name);
        }

        /// <summary>
        /// Gets the name of the property on the default sequence backing entity that holds the sequence value.
        /// </summary>
        public static string? GetDefaultEntitySequenceValueProperty(this IReadOnlyModel model)
        {
            return (string?)model[CalciteAnnotationNames.DefaultEntitySequenceValueProperty];
        }

        /// <summary>
        /// Sets the name of the property on the default sequence backing entity that holds the sequence value.
        /// </summary>
        public static void SetDefaultEntitySequenceValueProperty(this IMutableModel model, string? name)
        {
            model.SetOrRemoveAnnotation(CalciteAnnotationNames.DefaultEntitySequenceValueProperty, name);
        }

    }

}
