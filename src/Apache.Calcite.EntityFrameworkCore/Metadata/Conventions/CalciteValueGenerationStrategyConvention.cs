using System.Linq;

using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Metadata.Conventions
{

    public class CalciteValueGenerationStrategyConvention : IModelInitializedConvention, IModelFinalizingConvention
    {

        public CalciteValueGenerationStrategyConvention(ProviderConventionSetBuilderDependencies dependencies, RelationalConventionSetBuilderDependencies relationalDependencies)
        {
            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
        }

        protected virtual ProviderConventionSetBuilderDependencies Dependencies { get; }

        protected virtual RelationalConventionSetBuilderDependencies RelationalDependencies { get; }

        /// <inheritdoc/>
        public virtual void ProcessModelInitialized(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            modelBuilder.HasValueGenerationStrategy(CalciteValueGenerationStrategy.EntitySequenceHiLo);
        }

        /// <inheritdoc />
        public virtual void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
            {
                foreach (var property in entityType.GetDeclaredProperties())
                {
                    CalciteValueGenerationStrategy? strategy = null;
                    var declaringTable = property.GetMappedStoreObjects(StoreObjectType.Table).FirstOrDefault();
                    if (declaringTable.Name != null!)
                    {
                        strategy = property.GetValueGenerationStrategy(declaringTable, Dependencies.TypeMappingSource);
                        if (strategy == CalciteValueGenerationStrategy.None && !IsStrategyNoneNeeded(property, declaringTable))
                        {
                            strategy = null;
                        }
                    }
                    else
                    {
                        var declaringView = property.GetMappedStoreObjects(StoreObjectType.View).FirstOrDefault();
                        if (declaringView.Name != null!)
                        {
                            strategy = property.GetValueGenerationStrategy(declaringView, Dependencies.TypeMappingSource);
                            if (strategy == CalciteValueGenerationStrategy.None && !IsStrategyNoneNeeded(property, declaringView))
                            {
                                strategy = null;
                            }
                        }
                    }

                    if (strategy != null && declaringTable.Name != null)
                    {
                        property.Builder.HasValueGenerationStrategy(strategy);
                    }
                }
            }
        }
        bool IsStrategyNoneNeeded(IReadOnlyProperty property, StoreObjectIdentifier storeObject)
        {
            return false;
        }

    }

}