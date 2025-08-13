using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Project : BaseEntity
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

}
