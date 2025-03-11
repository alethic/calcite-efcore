using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public class CalciteSqlGenerationHelper : RelationalSqlGenerationHelper
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        public CalciteSqlGenerationHelper(RelationalSqlGenerationHelperDependencies dependencies) :
            base(dependencies)
        {

        }

    }

}
