using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("EnrolleeAchievement", Schema = "public")]
public class EnrolleeAchievement
{
    [Key]
    [Column("enrollee_achiev_id")]
    public long EnrolleeAchievementId { get; set; }

    [Column("enrollee_id")]
    public long EnrolleeId { get; set; }

    [Column("achievement_id")]
    public long AchievementId { get; set; }

    [ForeignKey("AchievementId")]
    public required virtual Achievement Achievement { get; set; }

    [ForeignKey("EnrolleeId")]
    public required virtual Enrollee Enrollee { get; set; }
}
