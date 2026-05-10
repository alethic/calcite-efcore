using Microsoft.EntityFrameworkCore.Query;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;

public class CompositeKeysQueryCalciteTest(CompositeKeysQueryCalciteFixture fixture)
    : CompositeKeysQueryRelationalTestBase<CompositeKeysQueryCalciteFixture>(fixture);

