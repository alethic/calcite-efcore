using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal
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

    }

}
