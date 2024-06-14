using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("EnrolleeSubject", Schema = "public")]
public class EnrolleeSubject
{
    [Key]
    [Column("enrollee_subject_id")]
    public long EnrolleeSubjectId { get; set; }

    [Column("enrollee_id")]
    public long EnrolleeId { get; set; }

    [Column("subject_id")]
    public long SubjectId { get; set; }

    [Column("result")]
    public int Result { get; set; }

    [ForeignKey("EnrolleeId")]
    public required virtual Enrollee Enrollee { get; set; }

    [ForeignKey("SubjectId")]
    public required virtual Subject Subject { get; set; }
}
