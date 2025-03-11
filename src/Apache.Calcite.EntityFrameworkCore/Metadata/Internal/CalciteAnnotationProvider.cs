using Microsoft.EntityFrameworkCore.Metadata;

namespace Apache.Calcite.EntityFrameworkCore.Metadata.Internal
{

    public class CalciteAnnotationProvider : RelationalAnnotationProvider
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteAnnotationProvider(RelationalAnnotationProviderDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
