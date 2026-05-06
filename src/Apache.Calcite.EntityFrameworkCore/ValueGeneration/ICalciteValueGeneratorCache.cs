using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    public interface ICalciteValueGeneratorCache : IValueGeneratorCache
    {

        CalciteEntitySequenceGeneratorState GetOrAddEntitySequenceState(IReadOnlyProperty property, IRelationalConnection connection);

    }

}
