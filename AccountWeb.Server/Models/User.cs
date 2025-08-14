using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

/// <summary>
/// 使用者
/// </summary>
public class User : BaseEntityWithTime
{
    /// <summary>
    /// 使用者ID
    /// </summary>
    [Required]
    [StringLength(50)]
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    [StringLength(255)]
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// 密碼
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;
    /// <summary>
    /// 對應廠區ID
    /// Factory.Id
    /// </summary>
    public int? FactoryId { get; set; }
    /// <summary>
    /// 信箱
    /// </summary>
    [StringLength(100)]
    public string? Email { get; set; }

    // Navigation properties
    public Factory? Factory { get; set; }
    public ICollection<UserGroupMapping> UserGroupMappings { get; set; } = new List<UserGroupMapping>();
    public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public ICollection<LoginRecord> LoginRecords { get; set; } = new List<LoginRecord>();
    public ICollection<AccountActionRecord> AccountActionRecords { get; set; } = new List<AccountActionRecord>();

}
