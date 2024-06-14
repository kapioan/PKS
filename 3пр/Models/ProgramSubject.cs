using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("ProgramSubject", Schema = "public")]
public class ProgramSubject
{
    [Key]
    [Column("program_subject_id")]
    public long ProgramSubjectId { get; set; }

    [Column("program_id")]
    public long ProgramId { get; set; }

    [Column("subject_Id")]
    public long SubjectId { get; set; }

    [Column("min_result")]
    public int MinResult { get; set; }

    [ForeignKey("ProgramId")]
    public virtual required Program Program { get; set; }

    [ForeignKey("SubjectId")]
    public virtual required Subject Subject { get; set; }
}
