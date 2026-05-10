// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests;
#nullable disable

public class MonsterFixupChangedOnlyCalciteTest(MonsterFixupChangedOnlyCalciteTest.MonsterFixupChangedOnlyCalciteFixture fixture)
    : MonsterFixupTestBase<
        MonsterFixupChangedOnlyCalciteTest.MonsterFixupChangedOnlyCalciteFixture>(fixture)
{
    public class MonsterFixupChangedOnlyCalciteFixture : MonsterFixupChangedOnlyFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => CalciteTestStoreFactory.Instance;

        protected override void OnModelCreating<TMessage, TProduct, TProductPhoto, TProductReview, TComputerDetail, TDimensions>(
            ModelBuilder builder)
        {
            base.OnModelCreating<TMessage, TProduct, TProductPhoto, TProductReview, TComputerDetail, TDimensions>(builder);

            builder.Entity<TMessage>().HasKey(e => e.MessageId);
            builder.Entity<TProductPhoto>().HasKey(e => e.PhotoId);
            builder.Entity<TProductReview>().HasKey(e => e.ReviewId);
        }
    }
}

