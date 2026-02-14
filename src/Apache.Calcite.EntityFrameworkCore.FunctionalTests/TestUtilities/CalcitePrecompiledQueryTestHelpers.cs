using System.Collections.Generic;
using System.Reflection;

using Apache.Calcite.EntityFrameworkCore.Infrastructure.Internal;

using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public class CalcitePrecompiledQueryTestHelpers : PrecompiledQueryTestHelpers
    {

        /// <inheritdoc/>
        public static readonly CalcitePrecompiledQueryTestHelpers Instance = new();

        /// <inheritdoc/>
        protected override IEnumerable<MetadataReference> BuildProviderMetadataReferences()
        {
            yield return MetadataReference.CreateFromFile(typeof(CalciteOptionsExtension).Assembly.Location);
            yield return MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location);
        }

    }

}
