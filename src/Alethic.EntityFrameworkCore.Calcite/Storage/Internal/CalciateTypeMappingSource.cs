using Microsoft.EntityFrameworkCore.Storage;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal
{

    public class CalciteTypeMappingSource : RelationalTypeMappingSource
    {

        public CalciteTypeMappingSource(
            TypeMappingSourceDependencies dependencies,
            RelationalTypeMappingSourceDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }
    }

}