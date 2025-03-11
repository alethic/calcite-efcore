using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="string"/>.
    /// </summary>
    public class CalciteByteArrayTypeMapping : ByteArrayTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteByteArrayTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteByteArrayTypeMapping() :
            base(SqlTypeName.VARBINARY.name(), System.Data.DbType.Binary)
        {

        }

    }

}