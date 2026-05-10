// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;
public class PrecompiledSqlPregenerationQuerySqlServerTest(
    PrecompiledSqlPregenerationQuerySqlServerTest.PrecompiledSqlPregenerationQueryCalciteFixture fixture,
    ITestOutputHelper testOutputHelper)
    : PrecompiledSqlPregenerationQueryRelationalTestBase(fixture, testOutputHelper),
        IClassFixture<PrecompiledSqlPregenerationQuerySqlServerTest.PrecompiledSqlPregenerationQueryCalciteFixture>
{
    protected override bool AlwaysPrintGeneratedSources
        => false;

    public class PrecompiledSqlPregenerationQueryCalciteFixture : PrecompiledSqlPregenerationQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public override PrecompiledQueryTestHelpers PrecompiledQueryTestHelpers
            => CalcitePrecompiledQueryTestHelpers.Instance;
    }
}

