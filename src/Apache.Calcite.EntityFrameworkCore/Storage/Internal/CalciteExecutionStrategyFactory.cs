using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteExecutionStrategyFactory : RelationalExecutionStrategyFactory
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteExecutionStrategyFactory(ExecutionStrategyDependencies dependencies) : base(dependencies)
        {

        }

        protected override IExecutionStrategy CreateDefaultStrategy(ExecutionStrategyDependencies dependencies)
            => new CalciteExecutionStrategy(dependencies);

    }

}
