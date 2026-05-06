using System;

using Apache.Calcite.EntityFrameworkCore.Extensions;
using Apache.Calcite.EntityFrameworkCore.Metadata;
using Apache.Calcite.EntityFrameworkCore.Storage.Internal;
using Apache.Calcite.EntityFrameworkCore.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    public class CalciteValueGeneratorSelector : RelationalValueGeneratorSelector
    {

        readonly ICalciteSequenceValueGeneratorFactory _sequenceFactory;
        readonly ICalciteConnection _connection;
        readonly IRelationalCommandDiagnosticsLogger _commandLogger;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="sequenceFactory"></param>
        /// <param name="connection"></param>
        /// <param name="commandLogger"></param>
        public CalciteValueGeneratorSelector(ValueGeneratorSelectorDependencies dependencies, ICalciteSequenceValueGeneratorFactory sequenceFactory, ICalciteConnection connection, IRelationalCommandDiagnosticsLogger commandLogger) :
            base(dependencies)
        {
            _sequenceFactory = sequenceFactory;
            _connection = connection;
            _commandLogger = commandLogger;
        }

        public new virtual ICalciteValueGeneratorCache Cache => (ICalciteValueGeneratorCache)base.Cache;

        /// <inheritdoc />
        [Obsolete("Use TrySelect and throw if needed when the generator is not found.")]
        public override ValueGenerator? Select(IProperty property, ITypeBase typeBase)
        {
            if (TrySelect(property, typeBase, out var valueGenerator))
            {
                return valueGenerator;
            }

            throw new NotSupportedException(CoreStrings.NoValueGenerator(property.Name, property.DeclaringType.DisplayName(), property.ClrType.ShortDisplayName()));
        }

        /// <inheritdoc />
        public override bool TrySelect(IProperty property, ITypeBase typeBase, out ValueGenerator? valueGenerator)
        {
            if (property.GetValueGeneratorFactory() != null || property.GetValueGenerationStrategy() != CalciteValueGenerationStrategy.EntitySequenceHiLo)
            {
                return base.TrySelect(property, typeBase, out valueGenerator);
            }

            var propertyType = property.ClrType.UnwrapNullableType().UnwrapEnumType();

            valueGenerator = _sequenceFactory.TryCreate(
                property,
                propertyType,
                Cache.GetOrAddEntitySequenceState(property, _connection),
                _commandLogger);

            if (valueGenerator != null)
            {
                return true;
            }

            var converter = property.GetTypeMapping().Converter;
            if (converter != null && converter.ProviderClrType != propertyType)
            {
                valueGenerator = _sequenceFactory.TryCreate(
                    property,
                    converter.ProviderClrType,
                    Cache.GetOrAddEntitySequenceState(property, _connection),
                    _commandLogger);

                if (valueGenerator != null)
                {
                    valueGenerator = valueGenerator.WithConverter(converter);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        protected override ValueGenerator? FindForType(IProperty property, ITypeBase typeBase, Type clrType)
            => property.ClrType.UnwrapNullableType() == typeof(Guid)
                ? property.ValueGenerated == ValueGenerated.Never || property.GetDefaultValueSql() != null
                    ? new TemporaryGuidValueGenerator()
                    : new SequentialGuidValueGenerator()
                : base.FindForType(property, typeBase, clrType);

    }

}
