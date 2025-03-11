using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Metadata.Conventions
{

    public class CalciteConventionSetBuilder : RelationalConventionSetBuilder
    {

        public CalciteConventionSetBuilder(ProviderConventionSetBuilderDependencies dependencies, RelationalConventionSetBuilderDependencies relationalDependencies) : base(dependencies, relationalDependencies)
        {

        }

    }

}
