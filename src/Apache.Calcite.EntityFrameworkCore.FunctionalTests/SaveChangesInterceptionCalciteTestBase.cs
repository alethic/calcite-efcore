// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public abstract class SaveChangesInterceptionSqliteTestBase(SaveChangesInterceptionSqliteTestBase.InterceptionSqliteFixtureBase fixture)
    : SaveChangesInterceptionTestBase(fixture)
{
    public abstract class InterceptionSqliteFixtureBase : InterceptionFixtureBase
    {
        protected override string StoreName
            => "SaveChangesInterception";

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override IServiceCollection InjectInterceptors(
            IServiceCollection serviceCollection,
            IEnumerable<IInterceptor> injectedInterceptors)
            => base.InjectInterceptors(serviceCollection.AddEntityFrameworkCalcite(), injectedInterceptors);
    }

    public class SaveChangesInterceptionCalciteTest(SaveChangesInterceptionCalciteTest.InterceptionCalciteFixture fixture)
        : SaveChangesInterceptionSqliteTestBase(fixture), IClassFixture<SaveChangesInterceptionCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => false;
        }
    }

    public class SaveChangesInterceptionWithDiagnosticsCalciteTest(
        SaveChangesInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture fixture)
        : SaveChangesInterceptionSqliteTestBase(fixture),
            IClassFixture<SaveChangesInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => true;
        }
    }
}

