using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

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

        public static IConventionPropertyBuilder? HasValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, in StoreObjectIdentifier storeObject, bool fromDataAnnotation = false)
        {
            if (propertyBuilder.CanSetValueGenerationStrategy(valueGenerationStrategy, storeObject, fromDataAnnotation))
            {
                propertyBuilder.Metadata.SetValueGenerationStrategy(valueGenerationStrategy, storeObject, fromDataAnnotation);
                return propertyBuilder;
            }

            return null;
        }

        public static bool CanSetValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, bool fromDataAnnotation = false)
        {
            return propertyBuilder.CanSetAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation);
        }

        public static bool CanSetValueGenerationStrategy(this IConventionPropertyBuilder propertyBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, in StoreObjectIdentifier storeObject, bool fromDataAnnotation = false)
        {
            return propertyBuilder.Metadata.FindOverrides(storeObject)?.Builder.CanSetAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation) ?? true;
        }

    }

}
