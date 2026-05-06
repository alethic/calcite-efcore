using System;
using System.Linq;

using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;
using Apache.Calcite.EntityFrameworkCore.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

    public static class CalcitePropertyExtensions
    {

        /// <summary>
        /// Gets the name of the sequence to use for value generation.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string? GetEntitySequenceName(this IReadOnlyProperty property)
        {
            return (string?)property[CalciteAnnotationNames.EntitySequenceName];
        }

        /// <summary>
        /// Sets the name of the entity sequence associated with the specified property.
        /// </summary>
        /// <param name="property">The property for which to set the entity sequence name. Cannot be null.</param>
        /// <param name="name">The name to assign to the entity sequence. Can be null to remove the sequence name annotation.</param>
        public static void SetEntitySequenceName(this IMutableProperty property, string name)
        {
            property.SetOrRemoveAnnotation(CalciteAnnotationNames.EntitySequenceName, name);
        }

        /// <summary>
        /// Sets the name of the entity sequence associated with the specified property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <param name="fromDataAnnotation"></param>
        /// <returns></returns>
        public static string? SetEntitySequenceName(this IConventionProperty property, string? name, bool fromDataAnnotation = false)
        {
            return (string?)property.SetOrRemoveAnnotation(CalciteAnnotationNames.EntitySequenceName, name, fromDataAnnotation)?.Value;
        }

        /// <summary>
        /// Gets the configuration source for the entity sequence name annotation applied to the specified property, if any.
        /// </summary>
        /// <param name="property">The property for which to retrieve the entity sequence name configuration source.</param>
        /// <returns>The configuration source for the entity sequence name annotation if it exists; otherwise, null.</returns>
        public static ConfigurationSource? GetEntitySequenceNameConfigurationSource(this IConventionProperty property)
        {
            return property.FindAnnotation(CalciteAnnotationNames.EntitySequenceName)?.GetConfigurationSource();
        }

        /// <summary>
        /// Finds the entity sequence associated with the specified property, if one is defined.
        /// </summary>
        /// <remarks>If the property does not have an explicitly defined entity sequence, the method
        /// searches for a sequence defined at the model level. Returns null if neither the property nor the model
        /// defines an entity sequence.</remarks>
        /// <param name="property">The property for which to locate the associated entity sequence.</param>
        /// <returns>An object representing the entity sequence associated with the property, or null if no sequence is defined.</returns>
        public static ICalciteEntitySequence? FindEntitySequence(this IReadOnlyProperty property)
        {
            var model = property.DeclaringType.Model;
            var sequenceName = property.GetEntitySequenceName() ?? model.GetEntitySequenceName();
            if (sequenceName is null)
                return null;

            return model.FindEntitySequence(sequenceName);
        }

        public static CalciteValueGenerationStrategy GetValueGenerationStrategy(this IReadOnlyProperty property)
        {
            var annotation = property.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy);
            if (annotation != null)
            {
                return (CalciteValueGenerationStrategy?)annotation.Value ?? CalciteValueGenerationStrategy.None;
            }

            var defaultValueGenerationStrategy = GetDefaultValueGenerationStrategy(property);

            if (property.ValueGenerated != ValueGenerated.OnAdd || property.IsForeignKey() || property.TryGetDefaultValue(out _) || property.GetDefaultValueSql() != null || property.GetComputedColumnSql() != null)
            {
                return CalciteValueGenerationStrategy.None;
            }

            return defaultValueGenerationStrategy;
        }

        public static CalciteValueGenerationStrategy GetValueGenerationStrategy(this IReadOnlyProperty property, in StoreObjectIdentifier storeObject)
        {
            return GetValueGenerationStrategy(property, storeObject, null);
        }

        internal static CalciteValueGenerationStrategy GetValueGenerationStrategy(this IReadOnlyProperty property, in StoreObjectIdentifier storeObject, ITypeMappingSource? typeMappingSource)
        {
            var @override = property.FindOverrides(storeObject)?.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy);
            if (@override != null)
            {
                return (CalciteValueGenerationStrategy?)@override.Value ?? CalciteValueGenerationStrategy.None;
            }

            var annotation = property.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy);
            if (annotation?.Value != null
                && StoreObjectIdentifier.Create(property.DeclaringType, storeObject.StoreObjectType) == storeObject)
            {
                return (CalciteValueGenerationStrategy)annotation.Value;
            }

            var table = storeObject;
            var sharedTableRootProperty = property.FindSharedStoreObjectRootProperty(storeObject);
            if (sharedTableRootProperty != null)
            {
                return CalciteValueGenerationStrategy.None;
            }

            if (property.ValueGenerated != ValueGenerated.OnAdd
                || table.StoreObjectType != StoreObjectType.Table
                || property.TryGetDefaultValue(storeObject, out _)
                || property.GetDefaultValueSql(storeObject) != null
                || property.GetComputedColumnSql(storeObject) != null
                || property.GetContainingForeignKeys()
                    .Any(fk =>
                        !fk.IsBaseLinking()
                        || (StoreObjectIdentifier.Create(fk.PrincipalEntityType, StoreObjectType.Table)
                                is { } principal
                            && fk.GetConstraintName(table, principal) != null)))
            {
                return CalciteValueGenerationStrategy.None;
            }

            var defaultStrategy = GetDefaultValueGenerationStrategy(property, storeObject, typeMappingSource);
            if (defaultStrategy != CalciteValueGenerationStrategy.None)
            {
                if (annotation != null)
                {
                    return (CalciteValueGenerationStrategy?)annotation.Value ?? CalciteValueGenerationStrategy.None;
                }
            }

            return defaultStrategy;
        }

        public static CalciteValueGenerationStrategy? GetValueGenerationStrategy(this IReadOnlyRelationalPropertyOverrides overrides)
        {
            return (CalciteValueGenerationStrategy?)overrides.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy)?.Value;
        }

        static CalciteValueGenerationStrategy GetDefaultValueGenerationStrategy(IReadOnlyProperty property)
        {
            var modelStrategy = property.DeclaringType.Model.GetValueGenerationStrategy();
            if (modelStrategy is CalciteValueGenerationStrategy.EntitySequenceHiLo && IsCompatibleWithValueGeneration(property))
            {
                return modelStrategy.Value;
            }

            return CalciteValueGenerationStrategy.None;
        }

        static CalciteValueGenerationStrategy GetDefaultValueGenerationStrategy(IReadOnlyProperty property, in StoreObjectIdentifier storeObject, ITypeMappingSource? typeMappingSource)
        {
            var modelStrategy = property.DeclaringType.Model.GetValueGenerationStrategy();
            if (modelStrategy is CalciteValueGenerationStrategy.EntitySequenceHiLo && IsCompatibleWithValueGeneration(property, storeObject, typeMappingSource))
            {
                return modelStrategy.Value;
            }

            return CalciteValueGenerationStrategy.None;
        }

        public static void SetValueGenerationStrategy(this IMutableProperty property, CalciteValueGenerationStrategy? value)
        {
            property.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value);
        }

        public static CalciteValueGenerationStrategy? SetValueGenerationStrategy(this IConventionProperty property, CalciteValueGenerationStrategy? value, bool fromDataAnnotation = false)
        {
            return (CalciteValueGenerationStrategy?)property.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation)?.Value;
        }

        public static void SetValueGenerationStrategy(this IMutableProperty property, CalciteValueGenerationStrategy? value, in StoreObjectIdentifier storeObject)
        {
            property.GetOrCreateOverrides(storeObject).SetValueGenerationStrategy(value);
        }

        public static CalciteValueGenerationStrategy? SetValueGenerationStrategy(this IConventionProperty property, CalciteValueGenerationStrategy? value, in StoreObjectIdentifier storeObject, bool fromDataAnnotation = false)
        {
            return property.GetOrCreateOverrides(storeObject, fromDataAnnotation).SetValueGenerationStrategy(value, fromDataAnnotation);
        }

        public static void SetValueGenerationStrategy(this IMutableRelationalPropertyOverrides overrides, CalciteValueGenerationStrategy? value)
        {
            overrides.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value);
        }

        public static CalciteValueGenerationStrategy? SetValueGenerationStrategy(this IConventionRelationalPropertyOverrides overrides, CalciteValueGenerationStrategy? value, bool fromDataAnnotation = false)
        {
            return (CalciteValueGenerationStrategy?)overrides.SetOrRemoveAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation)?.Value;
        }

        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(this IConventionProperty property)
        {
            return property.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();
        }

        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(this IConventionProperty property, in StoreObjectIdentifier storeObject)
        {
            return property.FindOverrides(storeObject)?.GetValueGenerationStrategyConfigurationSource();
        }

        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(this IConventionRelationalPropertyOverrides overrides)
        {
            return overrides.FindAnnotation(CalciteAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();
        }

        public static bool IsCompatibleWithValueGeneration(IReadOnlyProperty property)
        {
            var valueConverter = property.GetValueConverter() ?? property.FindTypeMapping()?.Converter;
            var type = (valueConverter?.ProviderClrType ?? property.ClrType).UnwrapNullableType();
            return type.IsInteger() || type.IsEnum || type == typeof(decimal);
        }

        static bool IsCompatibleWithValueGeneration(IReadOnlyProperty property, in StoreObjectIdentifier storeObject, ITypeMappingSource? typeMappingSource)
        {
            if (storeObject.StoreObjectType != StoreObjectType.Table)
                return false;

            var valueConverter = property.GetValueConverter() ?? (property.FindRelationalTypeMapping(storeObject) ?? typeMappingSource?.FindMapping((IProperty)property))?.Converter;
            var type = (valueConverter?.ProviderClrType ?? property.ClrType).UnwrapNullableType();
            return type.IsInteger() || type.IsEnum || type == typeof(decimal);
        }

    }

}
