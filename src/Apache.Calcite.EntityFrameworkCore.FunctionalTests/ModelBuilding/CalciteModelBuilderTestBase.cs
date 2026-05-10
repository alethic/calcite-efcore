// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;
namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.ModelBuilding;
public class SqliteModelBuilderTestBase : RelationalModelBuilderTest
{
    public abstract class SqliteNonRelationship(CalciteModelBuilderFixture fixture)
        : RelationalNonRelationshipTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>
    {
        [ConditionalFact]
        public void UseAutoincrement_sets_value_generation_strategy()
        {
            var modelBuilder = CreateModelBuilder();

            var propertyBuilder = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id);

            propertyBuilder.UseAutoincrement();

            Assert.Equal(SqliteValueGenerationStrategy.Autoincrement, propertyBuilder.Metadata.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void Generic_UseAutoincrement_sets_value_generation_strategy()
        {
            var modelBuilder = CreateModelBuilder();

            var propertyBuilder = modelBuilder
                .Entity<Customer>()
                .Property<int>(e => e.Id);

            propertyBuilder.UseAutoincrement();

            Assert.Equal(SqliteValueGenerationStrategy.Autoincrement, propertyBuilder.Metadata.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void Default_value_generation_strategy_for_integer_primary_key()
        {
            var modelBuilder = CreateModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .Metadata;

            var model = modelBuilder.FinalizeModel();

            // With conventions, integer primary keys should get autoincrement
            Assert.Equal(SqliteValueGenerationStrategy.Autoincrement, property.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_for_non_primary_key()
        {
            var modelBuilder = CreateModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.OtherId)
                .Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_for_non_integer_primary_key()
        {
            var modelBuilder = CreateModelBuilder();

            var property = modelBuilder
                .Entity<CustomerWithStringKey>()
                .Property(e => e.Id)
                .Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_for_composite_primary_key()
        {
            var modelBuilder = CreateModelBuilder();

            modelBuilder
                .Entity<CustomerWithCompositeKey>(b =>
                {
                    b.HasKey(e => new { e.Id1, e.Id2 });
                });

            var property1 = modelBuilder.Entity<CustomerWithCompositeKey>().Property(e => e.Id1).Metadata;
            var property2 = modelBuilder.Entity<CustomerWithCompositeKey>().Property(e => e.Id2).Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property1.GetValueGenerationStrategy());
            Assert.Equal(SqliteValueGenerationStrategy.None, property2.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_when_default_value_set()
        {
            var modelBuilder = CreateModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .HasDefaultValue(42)
                .Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_when_default_value_sql_set()
        {
            var modelBuilder = CreateModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .HasDefaultValueSql("1")
                .Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_when_computed_column_sql_set()
        {
            var modelBuilder = CreateModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .HasComputedColumnSql("1")
                .Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property.GetValueGenerationStrategy());
        }

        [ConditionalFact]
        public void No_autoincrement_when_property_is_foreign_key()
        {
            var modelBuilder = CreateModelBuilder();

            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.CustomerId);
                b.HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId);
            });

            var property = modelBuilder.Entity<Order>().Property(e => e.CustomerId).Metadata;

            var model = modelBuilder.FinalizeModel();

            Assert.Equal(SqliteValueGenerationStrategy.None, property.GetValueGenerationStrategy());
        }

        private class Customer
        {
            public int Id { get; set; }
            public int OtherId { get; set; }
            public string? Name { get; set; }
        }

        private class CustomerWithStringKey
        {
            public string Id { get; set; } = null!;
            public string? Name { get; set; }
        }

        private class CustomerWithCompositeKey
        {
            public int Id1 { get; set; }
            public int Id2 { get; set; }
            public string? Name { get; set; }
        }

        private class Order
        {
            public int Id { get; set; }
            public int CustomerId { get; set; }
        }
    }

    public abstract class SqliteComplexType(CalciteModelBuilderFixture fixture)
        : RelationalComplexTypeTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteComplexCollection(CalciteModelBuilderFixture fixture)
        : RelationalComplexCollectionTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteInheritance(CalciteModelBuilderFixture fixture)
        : RelationalInheritanceTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteOneToMany(CalciteModelBuilderFixture fixture)
        : RelationalOneToManyTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteManyToOne(CalciteModelBuilderFixture fixture)
        : RelationalManyToOneTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteOneToOne(CalciteModelBuilderFixture fixture)
        : RelationalOneToOneTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteManyToMany(CalciteModelBuilderFixture fixture)
        : RelationalManyToManyTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>;

    public abstract class SqliteOwnedTypes(CalciteModelBuilderFixture fixture)
        : RelationalOwnedTypesTestBase(fixture), IClassFixture<CalciteModelBuilderFixture>
    {
        public override void Can_use_sproc_mapping_with_owned_reference()
            => Assert.Equal(
                SqliteStrings.StoredProceduresNotSupported("Book.Label#BookLabel"),
                Assert.Throws<InvalidOperationException>(base.Can_use_sproc_mapping_with_owned_reference).Message);
    }

    public class CalciteModelBuilderFixture : RelationalModelBuilderFixture
    {
        public override TestHelpers TestHelpers
            => CalciteTestHelpers.Instance;
    }
}

