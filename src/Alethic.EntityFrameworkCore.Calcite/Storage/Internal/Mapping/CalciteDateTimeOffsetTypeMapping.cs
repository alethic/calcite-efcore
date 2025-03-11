using System;

using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="DateTimeOffset"/>.
    /// </summary>
    public class CalciteDateTimeOffsetTypeMapping : DateTimeOffsetTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteDateTimeOffsetTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteDateTimeOffsetTypeMapping() :
            base(SqlTypeName.TIMESTAMP_TZ.name())
        {

        }

    }

}