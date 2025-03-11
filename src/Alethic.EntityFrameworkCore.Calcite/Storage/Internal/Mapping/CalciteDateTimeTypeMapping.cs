using System;

using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="DateTime"/>.
    /// </summary>
    public class CalciteDateTimeTypeMapping : DateTimeTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteDateTimeTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteDateTimeTypeMapping() :
            base(SqlTypeName.TIMESTAMP.name())
        {

        }

    }

}