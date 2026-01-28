using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Apache.Calcite.EntityFrameworkCore.Extensions;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal
{

    /// <summary>
    /// Records options applied to the Calcite context.
    /// </summary>
    public class CalciteOptionsExtension : RelationalOptionsExtension
    {

        DbContextOptionsExtensionInfo? _info;
        JdbcProviderFactory? _jdbcProviderFactory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CalciteOptionsExtension()
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="copyFrom"></param>
        protected CalciteOptionsExtension(CalciteOptionsExtension copyFrom) :
            base(copyFrom)
        {

        }

        /// <inheritdoc />
        public override DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

        /// <inheritdoc />
        protected override RelationalOptionsExtension Clone() => new CalciteOptionsExtension(this);

        /// <summary>
        /// Gets the <see cref="JdbcProviderFactory"/> that will be used to initialize new connections.
        /// </summary>
        public virtual JdbcProviderFactory? JdbcProviderFactory => _jdbcProviderFactory;

        /// <summary>
        /// Sets the <see cref="JdbcProviderFactory"/> that will be used to initialize new connections.
        /// </summary>
        /// <param name="jdbcProviderFactory"></param>
        /// <returns></returns>
        public virtual CalciteOptionsExtension WithJdbcProviderFactory(JdbcProviderFactory jdbcProviderFactory)
        {
            var clone = (CalciteOptionsExtension)Clone();
            clone._jdbcProviderFactory = jdbcProviderFactory;
            return clone;
        }

        /// <inheritdoc />
        public override void ApplyServices(IServiceCollection services) => services.AddEntityFrameworkCalcite();

        sealed class ExtensionInfo(IDbContextOptionsExtension extension) : RelationalExtensionInfo(extension)
        {

            int? _serviceProviderHash;
            string? _logFragment;

            new CalciteOptionsExtension Extension => (CalciteOptionsExtension)base.Extension;

            /// <inheritdoc />
            public override bool IsDatabaseProvider => true;

            /// <inheritdoc />
            public override string LogFragment
            {
                get
                {
                    if (_logFragment == null)
                    {
                        var builder = new StringBuilder();
                        builder.Append(base.LogFragment);

                        if (Extension._jdbcProviderFactory != null)
                            builder.Append("JdbcProviderFactory ");

                        _logFragment = builder.ToString();
                    }

                    return _logFragment;
                }
            }

            /// <inheritdoc />
            public override int GetServiceProviderHashCode()
            {
                _serviceProviderHash ??= HashCode.Combine(
                    base.GetServiceProviderHashCode(),
                    3313,
                    Extension._jdbcProviderFactory);

                return _serviceProviderHash.Value;
            }

            /// <inheritdoc />
            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
                debugInfo["CalciteJdbc:" + nameof(JdbcProviderFactory)] = (Extension._jdbcProviderFactory?.GetHashCode() ?? 0L).ToString(CultureInfo.InvariantCulture);
            }

        }

    }

}
