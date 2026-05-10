using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.Diagnostics;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class CompositeKeyEndToEndCalciteTest(CompositeKeyEndToEndCalciteTest.CompositeKeyEndToEndCalciteFixture fixture) :
    CompositeKeyEndToEndTestBase<CompositeKeyEndToEndCalciteTest.CompositeKeyEndToEndCalciteFixture>(fixture)
{

    public class CompositeKeyEndToEndCalciteFixture : CompositeKeyEndToEndFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

