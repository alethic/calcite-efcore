using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;
using Apache.Calcite.EntityFrameworkCore.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Linq.Expressions;

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
        /// All entities will share the implicit default sequence unless they specify their own.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <returns>The same builder instance so that multiple calls can be chained.</returns>
        public static ModelBuilder UseHiLoEntitySequence(this ModelBuilder modelBuilder)
        {
            return UseHiLoEntitySequence(modelBuilder, Metadata.Conventions.CalciteValueGenerationStrategyConvention.DefaultSequenceName);
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

            // If the user has not registered a custom default backing entity, register the built-in CalciteSequence
            // entity now and add the default entity sequence eagerly so that runtime value generation can resolve it.
            var backingType = model.GetDefaultEntitySequenceEntityType();
            if (backingType == null)
            {
                modelBuilder.Entity<CalciteSequence>();
                model.SetDefaultEntitySequenceEntityType(typeof(CalciteSequence));
                model.SetDefaultEntitySequenceNameProperty(nameof(CalciteSequence.Name));
                model.SetDefaultEntitySequenceValueProperty(nameof(CalciteSequence.NextValue));
            }

            if (CalciteEntitySequence.FindEntitySequence(model, name) == null)
            {
                var entityClrType = model.GetDefaultEntitySequenceEntityType()!;
                var nameProp = model.GetDefaultEntitySequenceNameProperty()!;
                var valueProp = model.GetDefaultEntitySequenceValueProperty()!;
                var entityType = model.FindEntityType(entityClrType)!;
                var sequence = CalciteEntitySequence.AddEntitySequence(
                    model,
                    name,
                    entityType,
                    entityType.FindProperty(valueProp)!,
                    ConfigurationSource.Explicit);
                sequence.KeyValue = name;
            }

            return modelBuilder;
        }

        /// <summary>
        /// Registers a backing entity to be used as the source of automatically-generated per-entity HiLo sequences.
        /// Each entity that has a value-generated primary key will receive its own sequence row in this entity, keyed by entity name.
        /// </summary>
        /// <typeparam name="TEntity">The CLR type of the backing entity.</typeparam>
        /// <typeparam name="TValue">The CLR type of the sequence value property.</typeparam>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="namePropertyExpression">An expression selecting the property that identifies a sequence row by name.</param>
        /// <param name="valuePropertyExpression">An expression selecting the property that holds the sequence value.</param>
        /// <returns>The same builder instance so that multiple calls can be chained.</returns>
        public static ModelBuilder HasDefaultEntitySequenceEntity<TEntity, TValue>(
            this ModelBuilder modelBuilder,
            Expression<Func<TEntity, string>> namePropertyExpression,
            Expression<Func<TEntity, TValue>> valuePropertyExpression)
            where TEntity : class
        {
            Check.NotNull(modelBuilder);
            Check.NotNull(namePropertyExpression);
            Check.NotNull(valuePropertyExpression);

            var entity = modelBuilder.Entity<TEntity>();
            var nameProperty = entity.Metadata.FindProperty(namePropertyExpression.GetPropertyAccess())
                ?? throw new InvalidOperationException("The name property specified in the expression is not part of the entity.");
            var valueProperty = entity.Metadata.FindProperty(valuePropertyExpression.GetPropertyAccess())
                ?? throw new InvalidOperationException("The value property specified in the expression is not part of the entity.");

            var model = modelBuilder.Model;
            model.SetValueGenerationStrategy(CalciteValueGenerationStrategy.EntitySequenceHiLo);
            model.SetDefaultEntitySequenceEntityType(typeof(TEntity));
            model.SetDefaultEntitySequenceNameProperty(nameProperty.Name);
            model.SetDefaultEntitySequenceValueProperty(valueProperty.Name);
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
