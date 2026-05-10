// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.BulkUpdates.Inheritance;
#nullable disable

public class TPCInheritanceBulkUpdatesCalciteFixture : TPCInheritanceBulkUpdatesFixture
{
    protected override ITestStoreFactory TestStoreFactory
        => CalciteTestStoreFactory.Instance;

    public override bool UseGeneratedKeys
        => false;
}

