using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("Program", Schema = "public")]
public class Program
{
    [Key]
    [Column("program_id")]
    public long ProgramId { get; set; }

    [Column("name_program")]
    public required string Name { get; set; }

    [Column("department_id")]
    public long DepartmentId { get; set; }

    [Column("plan")]
    public int Plan { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual required Department Department { get; set; }
}
