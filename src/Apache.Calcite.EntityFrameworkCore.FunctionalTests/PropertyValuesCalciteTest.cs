using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class PropertyValuesCalciteTest(PropertyValuesCalciteTest.PropertyValuesCalciteFixture fixture)
    : PropertyValuesRelationalTestBase<PropertyValuesCalciteTest.PropertyValuesCalciteFixture>(fixture)
{
    public class PropertyValuesCalciteFixture : PropertyValuesRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

