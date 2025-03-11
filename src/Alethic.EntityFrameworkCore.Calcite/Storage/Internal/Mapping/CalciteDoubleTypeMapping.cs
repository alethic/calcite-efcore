using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="double"/>.
    /// </summary>
    public class CalciteDoubleTypeMapping : DoubleTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteDoubleTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteDoubleTypeMapping() :
            base(SqlTypeName.DOUBLE.name())
        {

        }

    }

}