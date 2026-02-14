using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public class ReflectiveSchemaNorthwindTestStoreFactory : CalciteTestStoreFactory
    {

        public static new ReflectiveSchemaNorthwindTestStoreFactory Instance { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected ReflectiveSchemaNorthwindTestStoreFactory()
        {

        }

        /// <inheritdoc/>
        public override TestStore GetOrCreate(string storeName) => ReflectiveSchemaNorthwindTestStore.GetExisting("northwind");

    }

}
