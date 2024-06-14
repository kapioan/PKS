using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("Subject", Schema = "public")]
public class Subject
{
    [Key]
    [Column("subject_id")]
    public long SubjectId { get; set; }

    [Column("name_subject")]
    public required string Name { get; set; }
}
