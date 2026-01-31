using System.Diagnostics;
using System.Linq;

using Apache.Calcite.EntityFrameworkCore.Tests.Csv;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spectre.Console;

namespace Apache.Calcite.EntityFrameworkCore.Tests
{

    [TestClass]
    public class DbContextTests
    {

        [TestMethod]
        public void CanSelectWithInclude()
        {
            var ts = new Stopwatch();

            for (int i = 0; i < 5; i++)
            {
                ts.Reset();
                ts.Start();
                using var db = new CsvDbContext();

                var tbl = new Table();
                tbl.AddColumn("Name");
                tbl.AddColumn("Gender");
                tbl.AddColumn("City");
                tbl.AddColumn("Age");
                tbl.AddColumn("Slacker");
                tbl.AddColumn("Join Date");
                tbl.AddColumn("Department");

                foreach (var employee in db.Employees.Include(i => i.Department))
                    tbl.AddRow([
                        employee.Name ?? "",
                        employee.Gender ?? "",
                        employee.City ?? "",
                        employee.Age.ToString(),
                        employee.Slacker.ToString(),
                        employee.JoinDate?.ToString(),
                        employee.Department?.Name ?? "",
                    ]);

                ts.Stop();
                tbl.Title = new TableTitle(ts.Elapsed.ToString(), Style.Plain);
                AnsiConsole.Write(tbl);
            }

            AnsiConsole.WriteLine("Done");
        }

    }

}
