using System.ComponentModel.DataAnnotations;

namespace APITemplate.Tools.Logger;

public class UserActivityLog
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string ActionType { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string IpAddress { get; set; } = default!;
}
