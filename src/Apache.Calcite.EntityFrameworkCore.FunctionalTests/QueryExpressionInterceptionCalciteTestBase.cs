// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public abstract class QueryExpressionInterceptionSqliteTestBase(
    QueryExpressionInterceptionSqliteTestBase.InterceptionSqliteFixtureBase fixture)
    : QueryExpressionInterceptionTestBase(fixture)
{
    public abstract class InterceptionSqliteFixtureBase : InterceptionFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override IServiceCollection InjectInterceptors(
            IServiceCollection serviceCollection,
            IEnumerable<IInterceptor> injectedInterceptors)
            => base.InjectInterceptors(serviceCollection.AddEntityFrameworkCalcite(), injectedInterceptors);
    }

    public class QueryExpressionInterceptionCalciteTest(QueryExpressionInterceptionCalciteTest.InterceptionCalciteFixture fixture)
        : QueryExpressionInterceptionSqliteTestBase(fixture), IClassFixture<QueryExpressionInterceptionCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override string StoreName
                => "QueryExpressionInterception";

            protected override bool ShouldSubscribeToDiagnosticListener
                => false;
        }
    }

    public class QueryExpressionInterceptionWithDiagnosticsCalciteTest(
        QueryExpressionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture fixture)
        : QueryExpressionInterceptionSqliteTestBase(fixture),
            IClassFixture<QueryExpressionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override string StoreName
                => "QueryExpressionInterceptionWithDiagnostics";

            protected override bool ShouldSubscribeToDiagnosticListener
                => true;
        }
    }
}

