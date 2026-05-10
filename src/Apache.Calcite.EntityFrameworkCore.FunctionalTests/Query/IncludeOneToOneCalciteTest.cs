// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;
#nullable disable

public class IncludeOneToOneCalciteTest(IncludeOneToOneCalciteTest.OneToOneQueryCalciteFixture fixture)
    : IncludeOneToOneTestBase<IncludeOneToOneCalciteTest.OneToOneQueryCalciteFixture>(fixture)
{
    public class OneToOneQueryCalciteFixture : OneToOneQueryFixtureBase, ITestSqlLoggerFactory
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;
    }
}

