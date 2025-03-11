// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.Infrastructure
{

    /// <summary>
    /// Allows Calcite specific configuration to be performed on <see cref="DbContextOptions" />.
    /// </summary>
    public class CalciteDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<CalciteDbContextOptionsBuilder, CalciteOptionsExtension>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CalciteDbContextOptionsBuilder" /> class.
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        public CalciteDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) :
            base(optionsBuilder)
        {

        }
    }

}
