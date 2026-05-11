using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteCommand : RelationalCommand
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="commandText"></param>
        /// <param name="logCommandText"></param>
        /// <param name="parameters"></param>
        public CalciteCommand(RelationalCommandBuilderDependencies dependencies, string commandText, string logCommandText, IReadOnlyList<IRelationalParameter> parameters) :
            base(dependencies, commandText, logCommandText, parameters)
        {

        }

        /// <inheritdoc/>
        public override int ExecuteNonQuery(RelationalCommandParameterObject parameterObject)
        {
            return base.ExecuteNonQuery(parameterObject);
        }

        /// <inheritdoc/>
        public override Task<int> ExecuteNonQueryAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken = default)
        {
            return base.ExecuteNonQueryAsync(parameterObject, cancellationToken);
        }

        /// <inheritdoc/>
        public override object? ExecuteScalar(RelationalCommandParameterObject parameterObject)
        {
            return base.ExecuteScalar(parameterObject);
        }

        /// <inheritdoc/>
        public override Task<object?> ExecuteScalarAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken = default)
        {
            return base.ExecuteScalarAsync(parameterObject, cancellationToken);
        }

        /// <inheritdoc/>
        public override RelationalDataReader ExecuteReader(RelationalCommandParameterObject parameterObject)
        {
            return base.ExecuteReader(parameterObject);
        }

        /// <inheritdoc/>
        public override Task<RelationalDataReader> ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken = default)
        {
            return base.ExecuteReaderAsync(parameterObject, cancellationToken);
        }

        /// <inheritdoc/>
        public override DbCommand CreateDbCommand(RelationalCommandParameterObject parameterObject, Guid commandId, DbCommandMethod commandMethod)
        {
            return base.CreateDbCommand(parameterObject, commandId, commandMethod);
        }

        /// <inheritdoc/>
        protected override RelationalDataReader CreateRelationalDataReader()
        {
            return base.CreateRelationalDataReader();
        }

    }

}
