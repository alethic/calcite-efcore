using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alethic.EntityFrameworkCore.Calcite.Tests.Csv
{

    [Table("EMPS")]
    public class CsvEmployee
    {

        [Key]
        [Column("EMPNO")]
        public int Id { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("DEPTNO")]
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public CsvDepartment? Department { get; set; }

        [Column("GENDER")]
        public string? Gender { get; set; }

        [Column("CITY")]
        public string? City { get; set; }

        [Column("AGE")]
        public int? Age { get; set; }

        [Column("SLACKER")]
        public bool? Slacker { get; set; }

        [Column("MANAGER")]
        public bool? Manager { get; set; }

        [Column("JOINEDAT")]
        public DateOnly? JoinDate { get; set; }

    }

}