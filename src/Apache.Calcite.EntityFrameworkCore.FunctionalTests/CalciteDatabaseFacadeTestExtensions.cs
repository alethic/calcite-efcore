using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests
{

    public static class CalciteDatabaseFacadeTestExtensions
    {

        public static void EnsureClean(this DatabaseFacade databaseFacade)
        {
            new CalciteDatabaseCleaner().Clean(databaseFacade);
        }

        /// <summary>
        /// Executes the named script.
        /// </summary>
        /// <param name="databaseFacade"></param>
        /// <param name="scriptName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task ExecuteScriptAsync(this DatabaseFacade databaseFacade, string scriptName, CancellationToken cancellationToken)
        {
            var res = typeof(CalciteDatabaseFacadeTestExtensions).Assembly.GetManifestResourceStream($"Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities.{scriptName}");
            if (res is not null)
            {
                using var rdr = new StreamReader(res);
                await foreach (var cmd in ReadScriptAsync(rdr, cancellationToken))
                    await databaseFacade.ExecuteSqlRawAsync(cmd, cancellationToken);
            }
        }

        static async IAsyncEnumerable<string> ReadScriptAsync(TextReader reader, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var cmd = new StringBuilder();

            while (true)
            {
                var txt = await reader.ReadLineAsync(cancellationToken);
                if (txt is null)
                {
                    if (cmd.Length > 0)
                        yield return cmd.ToString();

                    cmd = new StringBuilder();
                    break;
                }

                if (txt.Trim() == "GO")
                {
                    if (cmd.Length > 0)
                        yield return cmd.ToString();

                    cmd = new StringBuilder();
                    continue;
                }

                cmd.AppendLine(txt);
            }
        }


    }

}
