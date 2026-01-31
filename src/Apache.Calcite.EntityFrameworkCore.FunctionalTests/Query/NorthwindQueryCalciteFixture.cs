using System;

using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestModels.Northwind;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class NorthwindQueryCalciteFixture<TModelCustomizer> : NorthwindQueryRelationalFixture<TModelCustomizer>
        where TModelCustomizer : ITestModelCustomizer, new()
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteNorthwindTestStoreFactory.Instance;

        protected override Type ContextType
            => typeof(NorthwindCalciteContext);
    }

}
