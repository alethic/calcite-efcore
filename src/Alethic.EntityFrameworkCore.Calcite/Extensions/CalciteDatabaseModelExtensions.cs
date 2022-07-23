
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions
{

    public static class CalciteDatabaseModelExtensions
    {


        public static CalciteExtension GetOrAddCalciteExtension(
            this DatabaseModel model,
            string schema,
            string name,
            string version)
            => CalciteExtension.GetOrAddCalciteExtension(model, schema, name, version);

        public static IReadOnlyList<CalciteExtension> GetCalciteExtensions(this DatabaseModel model)
            => CalciteExtension.GetCalciteExtensions(model).ToArray();

    }

}
