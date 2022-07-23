using System.Collections.Generic;

using Alethic.EntityFrameworkCore.Calcite.Extensions;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal
{

    /// <summary>
    /// Represents options managed by Calcite.
    /// </summary>
    public class CalciteOptionsExtension : RelationalOptionsExtension
    {

        class ExtensionInfo : RelationalExtensionInfo
        {

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="extension"></param>
            public ExtensionInfo(IDbContextOptionsExtension extension) :
                base(extension)
            {

            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {

            }

        }

        ExtensionInfo info;

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
        public CalciteOptionsExtension(RelationalOptionsExtension copyFrom) :
            base(copyFrom)
        {

        }

        /// <inheritdoc />
        public override DbContextOptionsExtensionInfo Info => info ??= new ExtensionInfo(this);

        /// <inheritdoc />
        public override void ApplyServices(IServiceCollection services) => services.AddEntityFrameworkCalcite();

        /// <inheritdoc />
        protected override RelationalOptionsExtension Clone() => new CalciteOptionsExtension(this);

    }

}
