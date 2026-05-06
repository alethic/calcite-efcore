using System;
using System.Linq.Expressions;

namespace Apache.Calcite.EntityFrameworkCore.Metadata.Builders
{

    public class CalciteEntitySequenceBuilder<TEntity, TValue> : CalciteEntitySequenceBuilder
         where TEntity : class
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="sequence"></param>
        public CalciteEntitySequenceBuilder(ICalciteMutableEntitySequence sequence) :
            base(sequence)
        {

        }

        /// <summary>
        /// Sets the filter expression used to locate the single row in the entity table that represents this sequence.
        /// </summary>
        /// <param name="filter">A predicate expression identifying the sequence row.</param>
        /// <returns>The same builder so that multiple calls can be chained.</returns>
        public virtual CalciteEntitySequenceBuilder<TEntity, TValue> HasFilter(Expression<Func<TEntity, bool>> filter)
        {
            Metadata.EntityFilter = filter;
            return this;
        }

    }

}
