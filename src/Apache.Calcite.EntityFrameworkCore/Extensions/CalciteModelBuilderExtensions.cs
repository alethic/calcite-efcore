using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;
using Apache.Calcite.EntityFrameworkCore.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

    public static class CalciteModelBuilderExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entityType"></param>
        /// <param name="valueProperty"></param>
        /// <param name="name"></param>
        /// <param name="configurationSource"></param>
        /// <returns></returns>
        public static CalciteEntitySequence EntitySequence(this IMutableModel model, string name, IReadOnlyEntityType entityType, IReadOnlyProperty valueProperty, ConfigurationSource configurationSource)
        {
            Check.NotEmpty(name);
            Check.NotNull(entityType);

            var sequence = (CalciteEntitySequence?)CalciteEntitySequence.FindEntitySequence(model, name);
            if (sequence != null)
            {
                sequence.UpdateConfigurationSource(configurationSource);
                return sequence;
            }

            return CalciteEntitySequence.AddEntitySequence(model, name, entityType, valueProperty, configurationSource);
        }

        /// <summary>
        /// Configures the model to use a sequence-based hi-lo pattern to generate values for key properties marked as <see cref="ValueGenerated.OnAdd" />.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="name"></param>
        /// <returns>The same builder instance so that multiple calls can be chained.</returns>
        public static ModelBuilder UseHiLoEntitySequence(this ModelBuilder modelBuilder, string name)
        {
            var model = modelBuilder.Model;
            model.SetValueGenerationStrategy(CalciteValueGenerationStrategy.EntitySequenceHiLo);
            model.SetEntitySequenceName(name);
            return modelBuilder;
        }

        /// <summary>
        /// Configures the default value generation strategy for key properties marked as <see cref="ValueGenerated.OnAdd"/>.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="valueGenerationStrategy"></param>
        /// <param name="fromDataAnnotation"></param>
        /// <returns></returns>
        public static IConventionModelBuilder? HasValueGenerationStrategy(this IConventionModelBuilder modelBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, bool fromDataAnnotation = false)
        {
            if (modelBuilder.CanSetValueGenerationStrategy(valueGenerationStrategy, fromDataAnnotation))
            {
                modelBuilder.Metadata.SetValueGenerationStrategy(valueGenerationStrategy, fromDataAnnotation);
                return modelBuilder;
            }

            return null;
        }

        /// <summary>
        /// Returns a value indicating whether the given value can be set as the default value generation strategy.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="valueGenerationStrategy"></param>
        /// <param name="fromDataAnnotation"></param>
        /// <returns></returns>
        public static bool CanSetValueGenerationStrategy(this IConventionModelBuilder modelBuilder, CalciteValueGenerationStrategy? valueGenerationStrategy, bool fromDataAnnotation = false)
        {
            return modelBuilder.CanSetAnnotation(CalciteAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation);
        }

    }

}
