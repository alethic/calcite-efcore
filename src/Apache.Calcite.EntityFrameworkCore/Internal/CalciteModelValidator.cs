using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Metadata;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Apache.Calcite.EntityFrameworkCore.Internal
{

    public class CalciteModelValidator : RelationalModelValidator
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteModelValidator(ModelValidatorDependencies dependencies, RelationalModelValidatorDependencies relationalDependencies) : base(dependencies, relationalDependencies)
        {

        }

        /// <inheritdoc />
        public override void Validate(IModel model, IDiagnosticsLogger<DbLoggerCategory.Model.Validation> logger)
        {
            base.Validate(model, logger);

            ValidateDefaultEntitySequenceSchema(model);
        }

        /// <summary>
        /// Ensures that whenever the built-in <see cref="CalciteSequence"/> entity is used as the backing store
        /// for an entity-based HiLo sequence, a schema source is available for its mapped table. The schema may
        /// come from a model-level default schema or from an explicit schema assigned to the entity itself.
        /// Sequence objects in this provider are not real database objects; the requirement applies to the
        /// backing TABLE, not to the sequence abstraction.
        /// </summary>
        static void ValidateDefaultEntitySequenceSchema(IModel model)
        {
            var backingType = model.GetDefaultEntitySequenceEntityType();
            if (backingType != typeof(CalciteSequence))
                return;

            var entityType = model.FindEntityType(typeof(CalciteSequence));
            if (entityType == null)
                return;

            var schema = entityType.GetSchema() ?? model.GetDefaultSchema();
            if (!string.IsNullOrEmpty(schema))
                return;

            var tableName = entityType.GetTableName() ?? nameof(CalciteSequence);
            throw new System.InvalidOperationException(
                $"The built-in '{nameof(CalciteSequence)}' entity is being used as the backing store for an entity-based HiLo sequence, " +
                "but no schema could be inferred for it. Calcite requires the table to live in a writable schema. " +
                "Configure a schema source by either setting a model-level default schema (modelBuilder.HasDefaultSchema(\"...\")), " +
                $"assigning an explicit schema to the '{nameof(CalciteSequence)}' entity " +
                $"(modelBuilder.Entity<{nameof(CalciteSequence)}>().ToTable(\"{tableName}\", \"...\")), " +
                "or registering a custom backing entity via HasDefaultEntitySequenceEntity that is mapped to a schema.");
        }

    }

}
