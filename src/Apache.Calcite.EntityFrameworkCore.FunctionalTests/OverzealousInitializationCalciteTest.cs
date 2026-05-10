// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class OverzealousInitializationCalciteTest(OverzealousInitializationCalciteTest.OverzealousInitializationCalciteFixture fixture)
    : OverzealousInitializationTestBase<OverzealousInitializationCalciteTest.OverzealousInitializationCalciteFixture>(fixture)
{
    public class OverzealousInitializationCalciteFixture : OverzealousInitializationFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

