using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests
{

    public static class CalciteDatabaseFacadeTestExtensions
    {

        public static void EnsureClean(this DatabaseFacade databaseFacade)
            => new CalciteDatabaseCleaner().Clean(databaseFacade);

    }

}
