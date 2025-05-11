using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.Update.Internal
{

    public class CalciteModificationCommand : ModificationCommand
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="modificationCommandParameters"></param>
        public CalciteModificationCommand(in ModificationCommandParameters modificationCommandParameters) :
            base(modificationCommandParameters)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="modificationCommandParameters"></param>
        public CalciteModificationCommand(in NonTrackedModificationCommandParameters modificationCommandParameters) :
            base(modificationCommandParameters)
        {

        }

    }

}
