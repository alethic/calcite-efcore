using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage
{

    public class CalciteParameterNameGeneratorFactory : ParameterNameGeneratorFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteParameterNameGeneratorFactory(ParameterNameGeneratorDependencies dependencies) :
            base(dependencies)
        {

        }

        public override ParameterNameGenerator Create()
        {
            return new CalciteParameterNameGenerator();
        }

    }

}
