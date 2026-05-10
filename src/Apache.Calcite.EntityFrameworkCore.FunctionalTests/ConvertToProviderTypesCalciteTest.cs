// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class ConvertToProviderTypesCalciteTest : ConvertToProviderTypesTestBase<
    ConvertToProviderTypesCalciteTest.ConvertToProviderTypesCalciteFixture>
{
    public ConvertToProviderTypesCalciteTest(ConvertToProviderTypesCalciteFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        fixture.TestSqlLoggerFactory.Clear();
        fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    public class ConvertToProviderTypesCalciteFixture : ConvertToProviderTypesFixtureBase, ITestSqlLoggerFactory
    {
        public override bool StrictEquality
            => false;

        public override bool SupportsAnsi
            => false;

        public override bool SupportsUnicodeToAnsiConversion
            => true;

        public override bool SupportsLargeStringComparisons
            => true;

        public override bool SupportsDecimalComparisons
            => false;

        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        public override bool SupportsBinaryKeys
            => true;

        public override DateTime DefaultDateTime
            => new();

        public override bool PreservesDateTimeKind
            => true;
    }
}

