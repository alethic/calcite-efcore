using System.Linq;

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Metadata.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Metadata.Conventions
{

    public class CalciteValueGenerationConvention : RelationalValueGenerationConvention
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteValueGenerationConvention(ProviderConventionSetBuilderDependencies dependencies, RelationalConventionSetBuilderDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

        /// <inheritdoc/>
        public override void ProcessPropertyAnnotationChanged(IConventionPropertyBuilder propertyBuilder, string name, IConventionAnnotation? annotation, IConventionAnnotation? oldAnnotation, IConventionContext<IConventionAnnotation> context)
        {
            if (name == CalciteAnnotationNames.ValueGenerationStrategy)
            {
                propertyBuilder.ValueGenerated(GetValueGenerated(propertyBuilder.Metadata));
                return;
            }

            base.ProcessPropertyAnnotationChanged(propertyBuilder, name, annotation, oldAnnotation, context);
        }

        /// <inheritdoc/>
        protected override ValueGenerated? GetValueGenerated(IConventionProperty property)
        {
            var table = property.GetMappedStoreObjects(StoreObjectType.Table).FirstOrDefault();
            return table.Name != null
                    ? GetValueGenerated(property, table, Dependencies.TypeMappingSource)
                    : property.DeclaringType.IsMappedToJson()
#pragma warning disable EF1001 // Internal EF Core API usage.
                    && property.IsOrdinalKeyProperty()
#pragma warning restore EF1001 // Internal EF Core API usage.
                    && (property.DeclaringType as IReadOnlyEntityType)?.FindOwnership()!.IsUnique == false
                        ? ValueGenerated.OnAddOrUpdate
                        : property.GetMappedStoreObjects(StoreObjectType.InsertStoredProcedure).Any()
                            ? GetValueGenerated((IReadOnlyProperty)property)
                            : null;
        }

        /// <inheritdoc/>
        protected override bool MappingStrategyAllowsValueGeneration(IConventionProperty property, string? mappingStrategy)
        {
            return true;
        }

        /// <summary>
        /// Returns the store value generation strategy to set for the given property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="storeObject">The identifier of the store object.</param>
        /// <returns>The store value generation strategy to set for the given property.</returns>
        public static new ValueGenerated? GetValueGenerated(IReadOnlyProperty property, in StoreObjectIdentifier storeObject)
        {
            return RelationalValueGenerationConvention.GetValueGenerated(property, storeObject) ?? (property.GetValueGenerationStrategy(storeObject) != CalciteValueGenerationStrategy.None ? ValueGenerated.OnAdd : null);
        }

        static ValueGenerated? GetValueGenerated(IReadOnlyProperty property, in StoreObjectIdentifier storeObject, ITypeMappingSource typeMappingSource)
        {
            return RelationalValueGenerationConvention.GetValueGenerated(property, storeObject) ?? (property.GetValueGenerationStrategy(storeObject, typeMappingSource) != CalciteValueGenerationStrategy.None ? ValueGenerated.OnAdd : null);
        }

    }

}
