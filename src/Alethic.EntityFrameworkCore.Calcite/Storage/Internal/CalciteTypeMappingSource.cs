using System;
using System.Collections.Generic;

using Alethic.EntityFrameworkCore.Calcite.Storage.Internal.Mapping;

using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.sql.type;

namespace Alethic.EntityFrameworkCore.Calcite.Storage.Internal
{

    public class CalciteTypeMappingSource : RelationalTypeMappingSource
    {

        static readonly Dictionary<Type, RelationalTypeMapping> _clrTypeMappings = new()
        {
            [typeof(bool)] = CalciteBoolTypeMapping.Default,
            [typeof(byte)] = CalciteByteTypeMapping.Default,
            [typeof(sbyte)] = CalciteSByteTypeMapping.Default,
            [typeof(char)] = CalciteCharTypeMapping.Default,
            [typeof(short)] = CalciteShortTypeMapping.Default,
            [typeof(ushort)] = CalciteUShortTypeMapping.Default,
            [typeof(int)] = CalciteIntTypeMapping.Default,
            [typeof(uint)] = CalciteUIntTypeMapping.Default,
            [typeof(long)] = CalciteLongTypeMapping.Default,
            [typeof(ulong)] = CalciteULongTypeMapping.Default,
            [typeof(float)] = CalciteFloatTypeMapping.Default,
            [typeof(double)] = CalciteDoubleTypeMapping.Default,
            [typeof(decimal)] = CalciteDecimalTypeMapping.Default,
            [typeof(DateTime)] = CalciteDateTimeTypeMapping.Default,
            [typeof(DateTimeOffset)] = CalciteDateTimeOffsetTypeMapping.Default,
            [typeof(DateOnly)] = CalciteDateOnlyTypeMapping.Default,
            [typeof(TimeOnly)] = CalciteTimeOnlyTypeMapping.Default,
            [typeof(string)] = CalciteStringTypeMapping.Default,
            [typeof(byte[])] = CalciteByteArrayTypeMapping.Default,
        };

        static readonly Dictionary<string, RelationalTypeMapping[]> _storeTypeMappings = new(StringComparer.OrdinalIgnoreCase)
        {
            [SqlTypeName.BOOLEAN.name()] = [CalciteBoolTypeMapping.Default],
            [SqlTypeName.TINYINT.name()] = [CalciteByteTypeMapping.Default],
            [SqlTypeName.CHAR.name()] = [CalciteCharTypeMapping.Default],
            [SqlTypeName.SMALLINT.name()] = [CalciteShortTypeMapping.Default],
            [SqlTypeName.INTEGER.name()] = [CalciteIntTypeMapping.Default],
            [SqlTypeName.BIGINT.name()] = [CalciteLongTypeMapping.Default],
            [SqlTypeName.FLOAT.name()] = [CalciteFloatTypeMapping.Default],
            [SqlTypeName.DOUBLE.name()] = [CalciteDoubleTypeMapping.Default],
            [SqlTypeName.DECIMAL.name()] = [CalciteDecimalTypeMapping.Default],
            [SqlTypeName.DATE.name()] = [CalciteDateOnlyTypeMapping.Default],
            [SqlTypeName.TIME.name()] = [CalciteTimeOnlyTypeMapping.Default],
            [SqlTypeName.TIMESTAMP.name()] = [CalciteDateTimeTypeMapping.Default],
            [SqlTypeName.TIMESTAMP_TZ.name()] = [CalciteDateTimeOffsetTypeMapping.Default],
            [SqlTypeName.VARCHAR.name()] = [CalciteStringTypeMapping.Default],
            [SqlTypeName.VARBINARY.name()] = [CalciteByteArrayTypeMapping.Default],
        };

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public CalciteTypeMappingSource(TypeMappingSourceDependencies dependencies, RelationalTypeMappingSourceDependencies relationalDependencies) :
            base(dependencies, relationalDependencies)
        {

        }

        /// <inheritdoc/>
        protected override RelationalTypeMapping? FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var mapping = base.FindMapping(mappingInfo)
                ?? FindRawMapping(mappingInfo);

            return mapping != null
                && mappingInfo.StoreTypeName != null
                    ? mapping.WithStoreTypeAndSize(mappingInfo.StoreTypeName, null)
                    : mapping;
        }

        /// <summary>
        /// Finds the type mapping.
        /// </summary>
        /// <param name="mappingInfo"></param>
        /// <returns></returns>
        RelationalTypeMapping? FindRawMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            if (clrType != null && _clrTypeMappings.TryGetValue(clrType, out var mapping))
                return mapping;

            var storeTypeName = mappingInfo.StoreTypeName;
            if (storeTypeName != null && _storeTypeMappings.TryGetValue(storeTypeName, out var mappings))
                foreach (var m in mappings)
                if (clrType == null || (Nullable.GetUnderlyingType(m.ClrType) ?? m.ClrType) == clrType)
                    return m;

            var storeTypeNameBase = mappingInfo.StoreTypeNameBase;
            if (storeTypeNameBase != null && _storeTypeMappings.TryGetValue(storeTypeNameBase, out var baseMappings))
                foreach (var m in baseMappings)
                    if (clrType == null || (Nullable.GetUnderlyingType(m.ClrType) ?? m.ClrType) == clrType)
                        return m;

            return null;
        }

    }

}
