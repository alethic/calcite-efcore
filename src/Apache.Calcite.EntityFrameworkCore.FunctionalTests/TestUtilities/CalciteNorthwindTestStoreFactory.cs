using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public class CalciteNorthwindTestStoreFactory : CalciteTestStoreFactory
    {

        public static new CalciteNorthwindTestStoreFactory Instance { get; } = new();

        protected CalciteNorthwindTestStoreFactory()
        {

        }

        public override TestStore GetOrCreate(string storeName)
            => CalciteTestStore.GetExisting("northwind");

    }

}
