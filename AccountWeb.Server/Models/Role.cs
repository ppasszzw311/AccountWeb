using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Role : BaseEntityWithTime
{
    [Required]
    [StringLength(50)]
    public string RoleId { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string RoleName { get; set; } = string.Empty;
    [StringLength(255)]
    public string? Description { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
}
