using System;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Apache.Calcite.EntityFrameworkCore.Migrations
{

    /// <summary>
    /// Calcite-specific implementation of <see cref="MigrationsSqlGenerator" />.
    /// </summary>
    public class CalciteMigrationsSqlGenerator : MigrationsSqlGenerator
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies) :
            base(dependencies)
        {

        }

        /// <inheritdoc/>
        protected override void Generate(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            base.Generate(operation, model, builder, false);

            if (terminate)
            {
                EndStatement(builder);
            }
        }

        /// <inheritdoc/>
        protected override void CreateTablePrimaryKeyConstraint(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
        {
            
        }

        /// <inheritdoc/>
        protected override void CreateTableForeignKeys(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
        {
            
        }

        /// <inheritdoc />
        protected override void Generate(AlterSequenceOperation operation, IModel? model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        protected override void Generate(CreateIndexOperation operation, IModel? model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        protected override void Generate(DropIndexOperation operation, IModel? model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            throw new NotSupportedException();
        }

    }

}
