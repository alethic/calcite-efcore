using System;
using System.Data;
using System.Data.Common;

using IKVM.Jdbc.Data;

using java.sql;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spectre.Console;
using Spectre.Console.Rendering;

namespace Alethic.EntityFrameworkCore.Calcite.Tests
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
                tbl.AddColumn(new Spectre.Console.TableColumn(rdr.GetName(i));
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

    }

}
