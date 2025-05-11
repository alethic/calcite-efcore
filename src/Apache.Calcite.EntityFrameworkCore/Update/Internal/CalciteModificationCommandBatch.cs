using System.Threading;
using System.Threading.Tasks;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.Update.Internal
{

    public class CalciteModificationCommandBatch : ReaderModificationCommandBatch
    {


        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="maxBatchSize"></param>
        public CalciteModificationCommandBatch(ModificationCommandBatchFactoryDependencies dependencies, int maxBatchSize) :
            base(dependencies, maxBatchSize)
        {

        }

        /// <inheritdoc />
        protected override void Consume(RelationalDataReader reader)
        {
            var jdbcReader = (JdbcDataReader)reader.DbDataReader;
        }

        /// <inheritdoc />
        protected override Task ConsumeAsync(RelationalDataReader reader, CancellationToken cancellationToken = default)
        {
            Consume(reader);
            return Task.CompletedTask;
        }

    }

}
