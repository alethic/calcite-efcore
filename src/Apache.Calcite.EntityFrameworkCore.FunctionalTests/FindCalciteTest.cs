using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public abstract class FindCalciteTest(FindCalciteTest.FindCalciteFixture fixture) :
    FindTestBase<FindCalciteTest.FindCalciteFixture>(fixture)
{

    public class FindCalciteTestSet(FindCalciteFixture fixture) : FindCalciteTest(fixture)
    {

        protected override TestFinder Finder { get; } = new FindViaSetFinder();

    }

    public class FindCalciteTestContext(FindCalciteFixture fixture) : FindCalciteTest(fixture)
    {

        protected override TestFinder Finder { get; } = new FindViaContextFinder();

    }

    public class FindCalciteTestNonGeneric(FindCalciteFixture fixture) : FindCalciteTest(fixture)
    {

        protected override TestFinder Finder { get; } = new FindViaNonGenericContextFinder();

    }

    public class FindCalciteFixture : FindFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

    }

}

