using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Update;

namespace Apache.Calcite.EntityFrameworkCore.Update.Internal
{

    public class CalciteModificationCommandFactory : IModificationCommandFactory
    {
        public virtual IModificationCommand CreateModificationCommand(
            in ModificationCommandParameters modificationCommandParameters)
            => new CalciteModificationCommand(modificationCommandParameters);

        public virtual INonTrackedModificationCommand CreateNonTrackedModificationCommand(
            in NonTrackedModificationCommandParameters modificationCommandParameters)
            => new CalciteModificationCommand(modificationCommandParameters);
    }

}
