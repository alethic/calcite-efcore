using System.Collections.Generic;

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;

public abstract class QueryExpressionInterceptionCalciteTestBase(QueryExpressionInterceptionCalciteTestBase.InterceptionCalciteFixtureBase fixture) :
    QueryExpressionInterceptionTestBase(fixture)
{

    public abstract class InterceptionCalciteFixtureBase : InterceptionFixtureBase
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

        protected override IServiceCollection InjectInterceptors(IServiceCollection serviceCollection, IEnumerable<IInterceptor> injectedInterceptors) => base.InjectInterceptors(serviceCollection.AddEntityFrameworkCalcite(), injectedInterceptors);

    }

    public class QueryExpressionInterceptionCalciteTest(QueryExpressionInterceptionCalciteTest.InterceptionCalciteFixture fixture) :
        QueryExpressionInterceptionCalciteTestBase(fixture), IClassFixture<QueryExpressionInterceptionCalciteTest.InterceptionCalciteFixture>
    {

        public class InterceptionCalciteFixture : InterceptionCalciteFixtureBase
        {

            protected override string StoreName => "QueryExpressionInterception";

            protected override bool ShouldSubscribeToDiagnosticListener => false;

        }

    }

    public class QueryExpressionInterceptionWithDiagnosticsCalciteTest(QueryExpressionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture fixture) :
        QueryExpressionInterceptionCalciteTestBase(fixture), IClassFixture<QueryExpressionInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture>
    {

        public class InterceptionCalciteFixture : InterceptionCalciteFixtureBase
        {

            protected override string StoreName => "QueryExpressionInterceptionWithDiagnostics";

            protected override bool ShouldSubscribeToDiagnosticListener => true;

        }

    }

}
