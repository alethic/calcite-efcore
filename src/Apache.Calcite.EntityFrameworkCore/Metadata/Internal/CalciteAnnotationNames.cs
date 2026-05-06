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

        /// <summary>
        /// The CLR type name of the entity that backs default per-entity HiLo sequences for the model.
        /// </summary>
        public const string DefaultEntitySequenceEntityType = Prefix + nameof(DefaultEntitySequenceEntityType);

        /// <summary>
        /// The name of the property on the default sequence backing entity that identifies a sequence row.
        /// </summary>
        public const string DefaultEntitySequenceNameProperty = Prefix + nameof(DefaultEntitySequenceNameProperty);

        /// <summary>
        /// The name of the property on the default sequence backing entity that holds the sequence value.
        /// </summary>
        public const string DefaultEntitySequenceValueProperty = Prefix + nameof(DefaultEntitySequenceValueProperty);

    }

}
