// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query.Associations.ComplexTableSplitting;
public class ComplexTableSplittingPrimitiveCollectionCalciteTest(
    ComplexTableSplittingCalciteFixture fixture,
    ITestOutputHelper testOutputHelper)
    : ComplexTableSplittingPrimitiveCollectionRelationalTestBase<ComplexTableSplittingCalciteFixture>(fixture, testOutputHelper);

