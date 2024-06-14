using PKS4.Models.Data;

namespace PKS4.Models;

public class MenuViewModel
{
    public required User User { get; set; }
    public required List<Message> Messages { get; set; }
    public MessageFilterCriteria? FilterCriteria { get; set; }
}

public class MessageFilterCriteria
{
    public required string SenderLogin { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? Status { get; set; }
}

