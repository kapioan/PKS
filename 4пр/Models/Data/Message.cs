using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKS4.Models.Data;

[Table("Message", Schema = "public")]
public class Message
{
    [Key]
    [Column("message_id")]
    public long Id { get; set; }

    [Column("sender_id")]
    public long SenderId { get; set; }

    [Required]
    [ForeignKey("SenderId")]
    public required User Sender { get; set; }

    [Column("receiver_id")]
    public long ReceiverId { get; set; }

    [Required]
    [ForeignKey("ReceiverId")]
    public required User Receiver { get; set; }

    [Required]
    [Column("header")]
    public string Header { get; set; } = string.Empty;

    [Required]
    [Column("sending_date")]
    public DateTime SendingDate { get; set; } = DateTime.Now;

    [Required]
    [Column("status")]
    public int Status { get; set; }

    [Column("message_text")]
    public string MessageText { get; set; } = string.Empty;
}
