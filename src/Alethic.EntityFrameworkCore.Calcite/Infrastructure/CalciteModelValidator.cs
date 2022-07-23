using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Infrastructure
{

    /// <summary>
    /// The validator that enforces the rules for Calcite.
    /// </summary>
    public class CalciteModelValidator : RelationalModelValidator
    {

        /// <inheritdoc />
        public CalciteModelValidator(ModelValidatorDependencies dependencies, RelationalModelValidatorDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

    }

}
