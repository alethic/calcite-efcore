using Microsoft.EntityFrameworkCore.Update;

namespace Alethic.EntityFrameworkCore.Calcite.Update.Internal
{

    public class CalciteModificationCommand : ModificationCommand
    {

        public CalciteModificationCommand(in ModificationCommandParameters modificationCommandParameters) :
            base(modificationCommandParameters)
        {

        }

        public CalciteModificationCommand(in NonTrackedModificationCommandParameters modificationCommandParameters) :
            base(modificationCommandParameters)
        {

        }

        protected override void ProcessSinglePropertyJsonUpdate(ref ColumnModificationParameters parameters)
        {
            base.ProcessSinglePropertyJsonUpdate(ref parameters);
        }

    }

}
