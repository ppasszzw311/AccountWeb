using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Group : BaseEntityWithTime
{
    [Required]
    [StringLength(100)]
    public string GroupName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    [StringLength(255)]
    public string? Description { get; set; }

    // Navigation properties
    public Project Project { get; set; } = null!;
    public ICollection<UserGroupMapping> UserGroupMappings { get; set; } = new List<UserGroupMapping>();
}
