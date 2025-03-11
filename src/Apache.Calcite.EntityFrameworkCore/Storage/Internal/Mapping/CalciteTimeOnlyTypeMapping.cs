using System;

using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="TimeOnly"/>.
    /// </summary>
    public class CalciteTimeOnlyTypeMapping : TimeOnlyTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteTimeOnlyTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteTimeOnlyTypeMapping() :
            base(SqlTypeName.TIME.name())
        {

        }

    }

}