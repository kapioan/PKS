using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("Department", Schema = "public")]
public class Department
{
    [Key]
    [Column("department_id")]
    public long Id { get; set; }
    [Column("name_department")]
    public required string Name { get; set; }
}
