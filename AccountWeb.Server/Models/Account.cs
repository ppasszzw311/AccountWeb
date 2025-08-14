using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Account: BaseEntityWithTime
{
    [Required]
    [StringLength(100)]
    public string AccountId { get; set; } = string.Empty;
    [Required]
    [StringLength(255)]
    public string AccountPassword { get; set; } = string.Empty;
    public int OsType { get; set; } // 0: Windows, 1: Linux, 2: MacOS
    public int ProjectId { get; set; }
    [StringLength(45)]
    public string? ServerIp { get; set; } = string.Empty;
    public int? Port { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }

    // Navigation properties
    public Project Project { get; set; } = null!;
    public DomainTypes DomainType { get; set; } = null!;
    public ICollection<AccountActionRecord> AccountActionRecords = new List<AccountActionRecord>();

}
