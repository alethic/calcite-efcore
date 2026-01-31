using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public class CalciteTestStoreFactory : RelationalTestStoreFactory
    {

        public static CalciteTestStoreFactory Instance { get; } = new();

        protected CalciteTestStoreFactory()
        {

        }

        public override TestStore Create(string storeName)
            => CalciteTestStore.Create(storeName);

        public override TestStore GetOrCreate(string storeName)
            => CalciteTestStore.GetOrCreate(storeName);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection
                .AddEntityFrameworkCalcite();

    }

}
