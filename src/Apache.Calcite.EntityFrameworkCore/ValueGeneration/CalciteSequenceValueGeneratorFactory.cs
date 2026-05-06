using System;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    public class CalciteSequenceValueGeneratorFactory : ICalciteSequenceValueGeneratorFactory
    {

        readonly ICurrentDbContext _currentDbContext;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="currentDbContext"></param>
        public CalciteSequenceValueGeneratorFactory(ICurrentDbContext currentDbContext)
        {
            _currentDbContext = currentDbContext;
        }

        public virtual ValueGenerator? TryCreate(IProperty property, Type type, CalciteEntitySequenceGeneratorState generatorState, IRelationalCommandDiagnosticsLogger commandLogger)
        {
            if (type == typeof(long))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<long>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(int))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<int>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(decimal))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<decimal>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(short))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<short>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(byte))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<byte>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(char))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<char>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(ulong))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<ulong>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(uint))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<uint>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(ushort))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<ushort>(_currentDbContext, generatorState, commandLogger);
            }

            if (type == typeof(sbyte))
            {
                return new CalciteEntitySequenceHiLoValueGenerator<sbyte>(_currentDbContext, generatorState, commandLogger);
            }

            return null;
        }
    }

}
