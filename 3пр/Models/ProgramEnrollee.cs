using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("ProgramEnrollee", Schema = "public")]
public class ProgramEnrollee
{
    [Key]
    [Column("program_enrollee_id")]
    public long ProgramEnrolleeId { get; set; }

    [Column("program_id")]
    public long ProgramId { get; set; }

    [Column("enrollee_id")]
    public long EnrolleeId { get; set; }

    [ForeignKey("ProgramId")]
    public required virtual Program Program { get; set; }

    [ForeignKey("EnrolleeId")]
    public required virtual Enrollee Enrollee { get; set; }
}