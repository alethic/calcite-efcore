using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public class CalciteTestStoreFactory : RelationalTestStoreFactory
    {

        public static CalciteTestStoreFactory Instance { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected CalciteTestStoreFactory()
        {

        }

        /// <inheritdoc/>
        public override TestStore Create(string storeName) => CalciteTestStore.Create(storeName);

        /// <inheritdoc/>
        public override TestStore GetOrCreate(string storeName) => CalciteTestStore.GetOrCreate(storeName);

        /// <inheritdoc/>
        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkCalcite();
            serviceCollection.AddSingleton<IModelCustomizer, HiLoEntitySequenceModelCustomizer>();
            return serviceCollection;
        }

    }

}
