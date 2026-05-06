using Apache.Calcite.EntityFrameworkCore.Metadata;

using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Apache.Calcite.EntityFrameworkCore.ValueGeneration
{

    public class CalciteEntitySequenceGeneratorState : HiLoValueGeneratorState
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="entitySequence"></param>
        public CalciteEntitySequenceGeneratorState(ICalciteEntitySequence entitySequence) :
            base(entitySequence.IncrementBy)
        {
            EntitySequence = entitySequence;
        }

        public virtual ICalciteEntitySequence EntitySequence { get; }

    }

}
