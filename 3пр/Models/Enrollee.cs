using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("Enrollee", Schema = "public")]
public class Enrollee
{
    [Key]
    [Column("enrollee_id")]
    public long Id { get; set; }
    [Column("name_enrollee")]
    public required string Name { get; set; }
}
