using System.Threading.Tasks;
// ReSharper disable InconsistentNaming
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
using Xunit.Abstractions;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class PrecompiledQueryCalciteTest(
    PrecompiledQueryCalciteTest.PrecompiledQueryCalciteFixture fixture,
    ITestOutputHelper testOutputHelper)
    : PrecompiledQueryRelationalTestBase(fixture, testOutputHelper),
        IClassFixture<PrecompiledQueryCalciteTest.PrecompiledQueryCalciteFixture>
{
    protected override bool AlwaysPrintGeneratedSources
        => false;

    [ConditionalFact]
    public virtual Task Glob()
        => Test("""_ = context.Blogs.Where(b => EF.Functions.Glob(b.Name, "*foo*")).ToList();""");

    [ConditionalFact]
    public virtual Task Regexp()
        => Test("""_ = context.Blogs.Where(b => Regex.IsMatch(b.Name, "^foo")).ToList();""");

    public class PrecompiledQueryCalciteFixture : PrecompiledQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public override PrecompiledQueryTestHelpers PrecompiledQueryTestHelpers
            => CalcitePrecompiledQueryTestHelpers.Instance;
    }
}

