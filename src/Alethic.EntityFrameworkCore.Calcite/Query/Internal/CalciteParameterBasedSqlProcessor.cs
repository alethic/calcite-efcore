﻿using Microsoft.EntityFrameworkCore.Query;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteParameterBasedSqlProcessor : RelationalParameterBasedSqlProcessor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="parameters"></param>
        public CalciteParameterBasedSqlProcessor(RelationalParameterBasedSqlProcessorDependencies dependencies, RelationalParameterBasedSqlProcessorParameters parameters) :
            base(dependencies, parameters)
        {

        }

    }

}
