using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Project : BaseEntityWithTime
{
    [Required]
    [StringLength(50)]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// 廠區ID
    /// 對應Factory.Id
    /// </summary>
    public int FactoryId { get; set; }
    public string? Role { get; set; }

    // Navigation properties
    public Factory Factory { get; set; } = null!;
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
}
