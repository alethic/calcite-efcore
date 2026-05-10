// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public abstract class TransactionInterceptionSqliteTestBase(TransactionInterceptionSqliteTestBase.InterceptionSqliteFixtureBase fixture)
    : TransactionInterceptionTestBase(fixture)
{
    public abstract class InterceptionSqliteFixtureBase : InterceptionFixtureBase
    {
        protected override string StoreName
            => "TransactionInterception";

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override IServiceCollection InjectInterceptors(
            IServiceCollection serviceCollection,
            IEnumerable<IInterceptor> injectedInterceptors)
            => base.InjectInterceptors(serviceCollection.AddEntityFrameworkCalcite(), injectedInterceptors);
    }

    public class TransactionInterceptionCalciteTest(TransactionInterceptionCalciteTest.InterceptionCalciteFixture fixture)
        : TransactionInterceptionSqliteTestBase(fixture), IClassFixture<TransactionInterceptionCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => false;
        }
    }

    public class TransactionInterceptionWithDiagnosticsCalciteTest(
        TransactionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture fixture)
        : TransactionInterceptionSqliteTestBase(fixture),
            IClassFixture<TransactionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => true;
        }
    }
}

