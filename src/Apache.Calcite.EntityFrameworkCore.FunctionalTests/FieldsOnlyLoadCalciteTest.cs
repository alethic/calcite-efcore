// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class FieldsOnlyLoadCalciteTest(FieldsOnlyLoadCalciteTest.FieldsOnlyLoadCalciteFixture fixture)
    : FieldsOnlyLoadTestBase<FieldsOnlyLoadCalciteTest.FieldsOnlyLoadCalciteFixture>(fixture)
{
    public class FieldsOnlyLoadCalciteFixture : FieldsOnlyLoadFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

