using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="sbyte"/>.
    /// </summary>
    public class CalciteSByteTypeMapping : SByteTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteSByteTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteSByteTypeMapping() :
            base(SqlTypeName.TINYINT.name())
        {

        }

    }

}