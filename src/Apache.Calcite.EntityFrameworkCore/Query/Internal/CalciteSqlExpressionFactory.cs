﻿using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Internal
{

    /// <inheritdoc />
    public class CalciteSqlExpressionFactory : SqlExpressionFactory
    {

        /// <inheritdoc />
        public CalciteSqlExpressionFactory(SqlExpressionFactoryDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
