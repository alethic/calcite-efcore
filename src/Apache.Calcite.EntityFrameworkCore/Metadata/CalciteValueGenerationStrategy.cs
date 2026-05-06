namespace Apache.Calcite.EntityFrameworkCore.Metadata
{

    /// <summary>
    /// Defines strategies to use when generating values for database columns.
    /// </summary>
    public enum CalciteValueGenerationStrategy
    {

        /// <summary>
        /// No Calcite-specific strategy.
        /// </summary>
        None,

        /// <summary>
        /// A strategy where a separate table is used to generate values. This is the only strategy currently supported by Calcite.
        /// </summary>
        EntitySequenceHiLo,

    }

}
