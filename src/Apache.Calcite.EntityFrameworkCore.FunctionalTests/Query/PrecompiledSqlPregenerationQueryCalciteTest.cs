using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class PrecompiledSqlPregenerationQuerySqlServerTest(PrecompiledSqlPregenerationQuerySqlServerTest.PrecompiledSqlPregenerationQueryCalciteFixture fixture, ITestOutputHelper testOutputHelper) :
    PrecompiledSqlPregenerationQueryRelationalTestBase(fixture, testOutputHelper),
    IClassFixture<PrecompiledSqlPregenerationQuerySqlServerTest.PrecompiledSqlPregenerationQueryCalciteFixture>
{

    protected override bool AlwaysPrintGeneratedSources => false;

    public class PrecompiledSqlPregenerationQueryCalciteFixture : PrecompiledSqlPregenerationQueryRelationalFixture
    {

        protected override ITestStoreFactory TestStoreFactory => CalciteTestStoreFactory.Instance;

        public override PrecompiledQueryTestHelpers PrecompiledQueryTestHelpers => CalcitePrecompiledQueryTestHelpers.Instance;

    }

}

