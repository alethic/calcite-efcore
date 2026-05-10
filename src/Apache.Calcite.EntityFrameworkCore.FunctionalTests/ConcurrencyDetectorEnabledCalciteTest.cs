// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class ConcurrencyDetectorEnabledCalciteTest(ConcurrencyDetectorEnabledCalciteTest.ConcurrencyDetectorSqlServerFixture fixture)
    : ConcurrencyDetectorEnabledRelationalTestBase<
        ConcurrencyDetectorEnabledCalciteTest.ConcurrencyDetectorSqlServerFixture>(fixture)
{
    public class ConcurrencyDetectorSqlServerFixture : ConcurrencyDetectorFixtureBase, ITestSqlLoggerFactory
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;
    }
}

