// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query;
#nullable disable

public class InheritanceRelationshipsQueryCalciteTest(
    InheritanceRelationshipsQueryCalciteTest.InheritanceRelationshipsQueryCalciteFixture fixture) :
    InheritanceRelationshipsQueryRelationalTestBase<InheritanceRelationshipsQueryCalciteTest.InheritanceRelationshipsQueryCalciteFixture>(
        fixture)
{
    public class InheritanceRelationshipsQueryCalciteFixture : InheritanceRelationshipsQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;
    }
}

