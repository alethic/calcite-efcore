using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apache.Calcite.EntityFrameworkCore.Tests.Csv
{

    [Table("DEPTS")]
    public class CsvDepartment
    {

        [Key]
        [Column("DEPTNO")]
        public int? Id { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        public List<CsvEmployee>? Employees { get; set; }

    }

}
