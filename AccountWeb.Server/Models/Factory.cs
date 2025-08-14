using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Factory : BaseEntityWithTime
{
    [Required]
    [StringLength(50)]
    public string FactoryId { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string FactoryName { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
