using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class UserRoleMapping: BaseEntityWithTime
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    [Required]
    [StringLength(20)]
    public string ScopeType { get; set; } = string.Empty;
    public int ScopeId { get; set; } // e.g., FactoryId, ProjectId, etc.

    // Navigation properties
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
