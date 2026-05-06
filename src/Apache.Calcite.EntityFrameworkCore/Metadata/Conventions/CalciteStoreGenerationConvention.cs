using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Metadata.Conventions;

public class CalciteStoreGenerationConvention : StoreGenerationConvention
{

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="dependencies"></param>
    /// <param name="relationalDependencies"></param>
    public CalciteStoreGenerationConvention(ProviderConventionSetBuilderDependencies dependencies, RelationalConventionSetBuilderDependencies relationalDependencies) :
        base(dependencies, relationalDependencies)
    {

    }

    /// <inheritdoc />
    public override void ProcessPropertyAnnotationChanged(IConventionPropertyBuilder propertyBuilder, string name, IConventionAnnotation? annotation, IConventionAnnotation? oldAnnotation, IConventionContext<IConventionAnnotation> context)
    {
        if (annotation == null || oldAnnotation?.Value != null)
        {
            return;
        }

        var configurationSource = annotation.GetConfigurationSource();
        var fromDataAnnotation = configurationSource != ConfigurationSource.Convention;
        switch (name)
        {
            case RelationalAnnotationNames.DefaultValue:
                if (propertyBuilder.HasValueGenerationStrategy(null, fromDataAnnotation) == null && propertyBuilder.HasDefaultValue(null, fromDataAnnotation) != null)
                {
                    context.StopProcessing();
                    return;
                }

                break;
            case RelationalAnnotationNames.DefaultValueSql:
                if (propertyBuilder.HasValueGenerationStrategy(null, fromDataAnnotation) == null && propertyBuilder.HasDefaultValueSql(null, fromDataAnnotation) != null)
                {
                    context.StopProcessing();
                    return;
                }

                break;
            case RelationalAnnotationNames.ComputedColumnSql:
                if (propertyBuilder.HasValueGenerationStrategy(null, fromDataAnnotation) == null && propertyBuilder.HasComputedColumnSql(null, fromDataAnnotation) != null)
                {
                    context.StopProcessing();
                    return;
                }

                break;
            case CalciteAnnotationNames.ValueGenerationStrategy:
                if (((propertyBuilder.HasDefaultValue(null, fromDataAnnotation) == null || propertyBuilder.HasDefaultValueSql(null, fromDataAnnotation) == null || propertyBuilder.HasComputedColumnSql(null, fromDataAnnotation) == null) || (propertyBuilder.HasDefaultValue(null, fromDataAnnotation) == null || propertyBuilder.HasComputedColumnSql(null, fromDataAnnotation) == null)) && propertyBuilder.HasValueGenerationStrategy(null, fromDataAnnotation) != null)
                {
                    context.StopProcessing();
                    return;
                }

                break;
        }

        base.ProcessPropertyAnnotationChanged(propertyBuilder, name, annotation, oldAnnotation, context);
    }

    /// <inheritdoc />
    protected override void Validate(IConventionProperty property, in StoreObjectIdentifier storeObject)
    {
        if (property.GetValueGenerationStrategyConfigurationSource() != null)
        {
            var generationStrategy = property.GetValueGenerationStrategy(storeObject, Dependencies.TypeMappingSource);
            if (generationStrategy == CalciteValueGenerationStrategy.None)
            {
                base.Validate(property, storeObject);
                return;
            }

            if (property.TryGetDefaultValue(storeObject, out _))
            {
                // TODO
                //Dependencies.ValidationLogger.ConflictingValueGenerationStrategiesWarning(generationStrategy, "DefaultValue", property);
            }

            if (property.GetDefaultValueSql(storeObject) != null)
            {
                // TODO
                //Dependencies.ValidationLogger.ConflictingValueGenerationStrategiesWarning(generationStrategy, "DefaultValueSql", property);
            }

            if (property.GetComputedColumnSql(storeObject) != null)
            {
                // TODO
                //Dependencies.ValidationLogger.ConflictingValueGenerationStrategiesWarning(generationStrategy, "ComputedColumnSql", property);
            }
        }

        base.Validate(property, storeObject);
    }

}
