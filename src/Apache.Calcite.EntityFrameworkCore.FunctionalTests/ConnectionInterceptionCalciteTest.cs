// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
public abstract class ConnectionInterceptionSqliteTestBase(ConnectionInterceptionSqliteTestBase.InterceptionSqliteFixtureBase fixture)
    : ConnectionInterceptionTestBase(fixture)
{
    protected override DbContextOptionsBuilder ConfigureProvider(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCalcite();

    protected override BadUniverseContext CreateBadUniverse(DbContextOptionsBuilder optionsBuilder)
        => new(optionsBuilder.UseCalcite("Data Source=file:data.db?mode=invalidmode").Options);

    public abstract class InterceptionSqliteFixtureBase : InterceptionFixtureBase
    {
        protected override string StoreName
            => "ConnectionInterception";

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override IServiceCollection InjectInterceptors(
            IServiceCollection serviceCollection,
            IEnumerable<IInterceptor> injectedInterceptors)
            => base.InjectInterceptors(serviceCollection.AddEntityFrameworkCalcite(), injectedInterceptors);
    }

    public class ConnectionInterceptionCalciteTest(ConnectionInterceptionCalciteTest.InterceptionCalciteFixture fixture)
        : ConnectionInterceptionSqliteTestBase(fixture), IClassFixture<ConnectionInterceptionCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => false;
        }
    }

    public class ConnectionInterceptionWithDiagnosticsCalciteTest(
        ConnectionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture fixture)
        : ConnectionInterceptionSqliteTestBase(fixture),
            IClassFixture<ConnectionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => true;
        }
    }
}

