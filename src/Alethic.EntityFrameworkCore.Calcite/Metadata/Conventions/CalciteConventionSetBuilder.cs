using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Alethic.EntityFrameworkCore.Calcite.Metadata.Conventions
{

    public class CalciteConventionSetBuilder : RelationalConventionSetBuilder
    {

        public CalciteConventionSetBuilder(ProviderConventionSetBuilderDependencies dependencies, RelationalConventionSetBuilderDependencies relationalDependencies) : base(dependencies, relationalDependencies)
        {

        }

    }

}
