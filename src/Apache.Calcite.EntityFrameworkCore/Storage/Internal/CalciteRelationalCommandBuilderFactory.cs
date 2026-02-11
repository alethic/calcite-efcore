using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    /// <inheritdoc />
    public class CalciteRelationalCommandBuilderFactory : RelationalCommandBuilderFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteRelationalCommandBuilderFactory(RelationalCommandBuilderDependencies dependencies) :
            base(dependencies)
        {

        }

        /// <inheritdoc />
        public override IRelationalCommandBuilder Create()
        {
            return new CalciteRelationalCommandBuilder(Dependencies);
        }

    }

}
