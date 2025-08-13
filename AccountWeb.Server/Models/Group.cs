using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Group : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string GroupName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    [StringLength(255)]
    public string? Description { get; set; }
}
