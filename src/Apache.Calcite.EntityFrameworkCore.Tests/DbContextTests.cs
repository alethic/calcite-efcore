using System.Diagnostics;

using Apache.Calcite.EntityFrameworkCore.Tests.Csv;

using IKVM.Jdbc.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spectre.Console;

namespace Apache.Calcite.EntityFrameworkCore.Tests
{

    [TestClass]
    public class DbContextTests
    {

        [TestMethod]
        public void CanOpenCsv()
        {
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.adapter.csv.CsvSchemaFactory).Assembly);
            ikvm.runtime.Startup.addBootClassPathAssembly(typeof(org.apache.calcite.jdbc.Driver).Assembly);
            java.lang.Class.forName("org.apache.calcite.jdbc.Driver");

            using var cnn = new JdbcConnection("url=jdbc:calcite:model=Csv/model.json");
            cnn.Open();
            using var cmd = cnn.CreateCommand();
            cmd.CommandText = @"
SELECT      EMPS.EMPNO,
            EMPS.NAME,
            EMPS.GENDER,
            EMPS.CITY,
            EMPS.EMPID,
            EMPS.SLACKER,
            DEPTS.DEPTNO,
            DEPTS.NAME
FROM        EMPS
INNER JOIN  DEPTS
    ON      DEPTS.DEPTNO = EMPS.DEPTNO";
            using var rdr = cmd.ExecuteReader();

            var tbl = new Spectre.Console.Table();

            for (int i = 0; i < rdr.FieldCount; i++)
            {
                tbl.AddColumn(new Spectre.Console.TableColumn(rdr.GetName(i)));
            }

            while (rdr.Read())
            {
                var rcd = new string[rdr.FieldCount];
                for (int i = 0; i < rdr.FieldCount; i++)
                    rcd[i] = rdr.GetValue(i).ToString();

                tbl.AddRow(rcd);
            }

            AnsiConsole.Write(tbl);

            cnn.Open();
        }

        [TestMethod]
        public void CanOpenCsvEF()
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
