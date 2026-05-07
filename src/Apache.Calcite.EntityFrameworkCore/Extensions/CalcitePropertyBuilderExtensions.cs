using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

    /// <summary>
    /// Calcite-specific extension methods on <see cref="PropertyBuilder"/> and convention property builders for
    /// configuring HiLo value generation backed by an entity sequence.
    /// </summary>
    public static class CalcitePropertyBuilderExtensions
    {

        /// <summary>
        /// Configures the property to use a HiLo value generation strategy based on a sequence entity with the
        /// specified name.
        /// </summary>
        /// <remarks>This method enables HiLo-based value generation for the property, which can improve
        /// performance by reducing database round-trips when generating key values. The specified sequence entity must
        /// exist in the model.</remarks>
        /// <param name="propertyBuilder">The builder used to configure the property.</param>
        /// <param name="name">The name of the sequence entity to use for HiLo value generation. Cannot be null or empty.</param>
        /// <returns>The same PropertyBuilder instance so that additional configuration calls can be chained.</returns>
        public static PropertyBuilder UseHiLoEntitySequence(this PropertyBuilder propertyBuilder, string name)
        {
            var property = propertyBuilder.Metadata;
            var model = property.DeclaringType.Model;

            property.SetValueGenerationStrategy(CalciteValueGenerationStrategy.EntitySequenceHiLo);
            property.SetEntitySequenceName(name);

            return propertyBuilder;
        }

        /// <summary>
        /// Returns whether the given name can be set as the sequence entity name on the property.
        /// </summary>
        /// <param name="propertyBuilder"></param>
        /// <param name="name"></param>
        /// <param name="fromDataAnnotation"></param>
        /// <returns></returns>
        public static bool CanSetEntitySequenceName(this IConventionPropertyBuilder propertyBuilder, string? name, bool fromDataAnnotation = false)
        {
            return propertyBuilder.CanSetAnnotation(CalciteAnnotationNames.EntitySequenceName, name, fromDataAnnotation);
        }

        /// <summary>
        /// Sets the Calcite value generation strategy on the convention property builder.
        /// </summary>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="valueGenerationStrategy">The strategy to apply.</param>
        /// <param name="fromDataAnnotation">Whether the configuration originates from a data annotation.</param>
        /// <returns>The same builder if the strategy was set; otherwise <see langword="null"/>.</returns>
        public static IConventionPropertyBuilder? HasValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, bool fromDataAnnotation = false)
        {
            if (propertyBuilder.CanSetAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation))
            {
                propertyBuilder.Metadata.SetValueGenerationStrategy(valueGenerationStrategy, fromDataAnnotation);

                if (valueGenerationStrategy != CalciteValueGenerationStrategy.EntitySequenceHiLo)
                {
                    // Clear the sequence entity type if the value generation strategy is not SequenceEntityHiLo
                }

                return propertyBuilder;
            }

            return null;
        }

        /// <summary>
        /// Sets the Calcite value generation strategy on the convention property builder for a specific store object.
        /// </summary>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="valueGenerationStrategy">The strategy to apply.</param>
        /// <param name="storeObject">The target store object.</param>
        /// <param name="fromDataAnnotation">Whether the configuration originates from a data annotation.</param>
        /// <returns>The same builder if the strategy was set; otherwise <see langword="null"/>.</returns>
        public static IConventionPropertyBuilder? HasValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, in StoreObjectIdentifier storeObject, bool fromDataAnnotation = false)
        {
            if (propertyBuilder.CanSetValueGenerationStrategy(valueGenerationStrategy, storeObject, fromDataAnnotation))
            {
                propertyBuilder.Metadata.SetValueGenerationStrategy(valueGenerationStrategy, storeObject, fromDataAnnotation);
                return propertyBuilder;
            }

            return null;
        }

        /// <summary>
        /// Returns whether the given Calcite value generation strategy can be set on the property.
        /// </summary>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="valueGenerationStrategy">The strategy to validate.</param>
        /// <param name="fromDataAnnotation">Whether the configuration originates from a data annotation.</param>
        /// <returns><see langword="true"/> if the strategy can be set; otherwise <see langword="false"/>.</returns>
        public static bool CanSetValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, bool fromDataAnnotation = false)
        {
            return propertyBuilder.CanSetAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation);
        }

        /// <summary>
        /// Returns whether the given Calcite value generation strategy can be set on the property for the specified store object.
        /// </summary>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="valueGenerationStrategy">The strategy to validate.</param>
        /// <param name="storeObject">The target store object.</param>
        /// <param name="fromDataAnnotation">Whether the configuration originates from a data annotation.</param>
        /// <returns><see langword="true"/> if the strategy can be set; otherwise <see langword="false"/>.</returns>
        public static bool CanSetValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, in StoreObjectIdentifier storeObject, bool fromDataAnnotation = false)
        {
            return propertyBuilder.Metadata.FindOverrides(storeObject)?.Builder.CanSetAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation) ?? true;
        }

    }

}
