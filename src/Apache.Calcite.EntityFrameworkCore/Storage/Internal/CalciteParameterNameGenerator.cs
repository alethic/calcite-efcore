using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteParameterNameGenerator : ParameterNameGenerator
    {


        int _count = 1;

        /// <summary>
        /// Generates the next unique parameter name.
        /// </summary>
        /// <returns>The generated name.</returns>
        public override string GenerateNext() => "" + _count++;

        /// <summary>
        /// Resets the generator, meaning it can reuse previously generated names.
        /// </summary>
        public override void Reset() => _count = 1;

    }

}
