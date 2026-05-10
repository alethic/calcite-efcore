// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class DesignTimeCalciteTest(DesignTimeCalciteTest.DesignTimeCalciteFixture fixture)
    : DesignTimeTestBase<DesignTimeCalciteTest.DesignTimeCalciteFixture>(fixture)
{
    protected override Assembly ProviderAssembly
        => typeof(SqliteDesignTimeServices).Assembly;

    public class DesignTimeCalciteFixture : DesignTimeFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

