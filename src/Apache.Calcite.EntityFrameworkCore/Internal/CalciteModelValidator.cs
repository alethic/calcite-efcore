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
        public CalciteModelValidator(ModelValidatorDependencies dependencies, RelationalModelValidatorDependencies relationalDependencies) : 
            base(dependencies, relationalDependencies)
        {

        }

        /// <inheritdoc />
        public override void Validate(IModel model, IDiagnosticsLogger<DbLoggerCategory.Model.Validation> logger)
        {
            base.Validate(model, logger);
        }

    }

}
