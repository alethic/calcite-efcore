using System.Collections.Generic;
using System.Text;

using Alethic.EntityFrameworkCore.Calcite.Extensions;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;

public class CalciteOptionsExtension : RelationalOptionsExtension
{

    DbContextOptionsExtensionInfo? _info;

    public CalciteOptionsExtension()
    {

    }

    protected CalciteOptionsExtension(CalciteOptionsExtension copyFrom)
        : base(copyFrom)
    {

    }

    public override DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

    protected override RelationalOptionsExtension Clone() => new CalciteOptionsExtension(this);

    public override void ApplyServices(IServiceCollection services) => services.AddEntityFrameworkCalcite();

    sealed class ExtensionInfo(IDbContextOptionsExtension extension) : RelationalExtensionInfo(extension)
    {

        string? _logFragment;

        new CalciteOptionsExtension Extension => (CalciteOptionsExtension)base.Extension;

        public override bool IsDatabaseProvider => true;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => other is ExtensionInfo;

        public override string LogFragment
        {
            get
            {
                if (_logFragment == null)
                {
                    var builder = new StringBuilder();

                    builder.Append(base.LogFragment);

                    _logFragment = builder.ToString();
                }

                return _logFragment;
            }
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) => debugInfo["Calcite"] = "1";

    }

}
