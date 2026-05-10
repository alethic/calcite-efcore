// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public abstract class CommandInterceptionSqliteTestBase(CommandInterceptionSqliteTestBase.InterceptionSqliteFixtureBase fixture)
    : CommandInterceptionTestBase(fixture)
{
    public override async Task<string> Intercept_query_passively(bool async, bool inject)
    {
        AssertSql(
            """
SELECT "s"."Id", "s"."Type" FROM "Singularity" AS "s"
""",
            await base.Intercept_query_passively(async, inject));

        return null;
    }

    protected override async Task<string> QueryMutationTest<TInterceptor>(bool async, bool inject)
    {
        AssertSql(
            """
SELECT "s"."Id", "s"."Type" FROM "Brane" AS "s"
""",
            await base.QueryMutationTest<TInterceptor>(async, inject));

        return null;
    }

    public override async Task<string> Intercept_query_to_replace_execution(bool async, bool inject)
    {
        AssertSql(
            """
SELECT "s"."Id", "s"."Type" FROM "Singularity" AS "s"
""",
            await base.Intercept_query_to_replace_execution(async, inject));

        return null;
    }

    public abstract class InterceptionSqliteFixtureBase : InterceptionFixtureBase
    {
        protected override string StoreName
            => "CommandInterception";

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override IServiceCollection InjectInterceptors(
            IServiceCollection serviceCollection,
            IEnumerable<IInterceptor> injectedInterceptors)
            => base.InjectInterceptors(serviceCollection.AddEntityFrameworkCalcite(), injectedInterceptors);
    }

    public class CommandInterceptionCalciteTest(CommandInterceptionCalciteTest.InterceptionCalciteFixture fixture)
        : CommandInterceptionSqliteTestBase(fixture), IClassFixture<CommandInterceptionCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => false;
        }
    }

    public class CommandInterceptionWithDiagnosticsCalciteTest(
        CommandInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture fixture)
        : CommandInterceptionSqliteTestBase(fixture), IClassFixture<CommandInterceptionWithDiagnosticsCalciteTest.InterceptionCalciteFixture>
    {
        public class InterceptionCalciteFixture : InterceptionSqliteFixtureBase
        {
            protected override bool ShouldSubscribeToDiagnosticListener
                => true;
        }
    }
}

