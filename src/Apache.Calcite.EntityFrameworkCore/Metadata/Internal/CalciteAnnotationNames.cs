namespace Apache.Calcite.EntityFrameworkCore.Metadata.Internal
{

    public static class CalciteAnnotationNames
    {

        public const string Prefix = "calcite:";

        public const string ValueGenerationStrategy = Prefix + nameof(ValueGenerationStrategy);

        /// <summary>
        /// Represents the configuration key for entity sequence settings of the model.
        /// </summary>
        public const string EntitySequences = Prefix + nameof(EntitySequences);

        /// <summary>
        /// The name of the sequence entity to use as a source of value generation.
        /// </summary>
        public const string EntitySequenceName = Prefix + nameof(EntitySequenceName);

    }

}
