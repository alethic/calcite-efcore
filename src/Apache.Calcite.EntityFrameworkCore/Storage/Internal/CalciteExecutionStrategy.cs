using System;
using System.Threading;
using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.Properties;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteExecutionStrategy : IExecutionStrategy
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteExecutionStrategy(ExecutionStrategyDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        ExecutionStrategyDependencies Dependencies { get; }

        /// <inheritdoc />
        public virtual bool RetriesOnFailure => false;

        /// <inheritdoc />
        public virtual TResult Execute<TState, TResult>(TState state, Func<DbContext, TState, TResult> operation, Func<DbContext, TState, ExecutionResult<TResult>>? verifySucceeded)
        {
            try
            {
                return operation(Dependencies.CurrentContext.Context, state);
            }
            catch (Exception ex) when (ExecutionStrategy.CallOnWrappedException(ex, CalciteTransientExceptionDetector.ShouldRetryOn))
            {
                throw new InvalidOperationException(CalciteStrings.TransientExceptionDetected, ex);
            }
        }

        /// <inheritdoc />
        public virtual async Task<TResult> ExecuteAsync<TState, TResult>(TState state, Func<DbContext, TState, CancellationToken, Task<TResult>> operation, Func<DbContext, TState, CancellationToken, Task<ExecutionResult<TResult>>>? verifySucceeded, CancellationToken cancellationToken)
        {
            try
            {
                return await operation(Dependencies.CurrentContext.Context, state, cancellationToken);
            }
            catch (Exception ex) when (ExecutionStrategy.CallOnWrappedException(ex, CalciteTransientExceptionDetector.ShouldRetryOn))
            {
                throw new InvalidOperationException(CalciteStrings.TransientExceptionDetected, ex);
            }
        }

    }

}
