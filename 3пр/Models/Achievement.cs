using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Models;

[Table("Achievement", Schema = "public")]
public class Achievement
{
    [Key]
    [Column("achievement_id")]
    public long Id { get; set; }
    [Column("name_achievement")]
    public required string Name { get; set; }
    [Column("bonus")]
    public short Bonus { get; set; }
}
