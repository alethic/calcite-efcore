using Microsoft.EntityFrameworkCore.Query;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class CompositeKeysSplitQueryCalciteTest(CompositeKeysQueryCalciteFixture fixture)
    : CompositeKeysSplitQueryRelationalTestBase<CompositeKeysQueryCalciteFixture>(fixture);

