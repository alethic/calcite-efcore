using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping
{

    /// <summary>
    /// Maps <see cref="bool"/>.
    /// </summary>
    public class CalciteBoolTypeMapping : BoolTypeMapping, ICalciteTypeMapping
    {

        /// <summary>
        /// Gets the default instance of this type mapping.
        /// </summary>
        public static new CalciteBoolTypeMapping Default { get; } = new();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteBoolTypeMapping() :
            base(SqlTypeName.BOOLEAN.name())
        {

        }

    }

}