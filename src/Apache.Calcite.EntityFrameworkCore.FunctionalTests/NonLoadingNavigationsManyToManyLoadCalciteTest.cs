using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public class NonLoadingNavigationsManyToManyLoadCalciteTest(NonLoadingNavigationsManyToManyLoadCalciteTest.NonLoadingNavigationsManyToManyLoadCalciteFixture fixture) :
    ManyToManyLoadTestBase<NonLoadingNavigationsManyToManyLoadCalciteTest.NonLoadingNavigationsManyToManyLoadCalciteFixture>(fixture)
{

    public class NonLoadingNavigationsManyToManyLoadCalciteFixture : ManyToManyLoadFixtureBase, ITestSqlLoggerFactory
    {

        protected override string StoreName => "NonLoadingNavigationsManyToMany";

        public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder) => base.AddOptions(builder).UseLazyLoadingProxies();

        protected override IServiceCollection AddServices(IServiceCollection serviceCollection) => base.AddServices(serviceCollection.AddEntityFrameworkProxies());

    }

}

