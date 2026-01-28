using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration.Internal
{

    public class CalciteValueGeneratorSelector : RelationalValueGeneratorSelector
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteValueGeneratorSelector(ValueGeneratorSelectorDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
