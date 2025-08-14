using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;
public class DomainCategory : BaseEntityWithTime
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty; // Category name

    // Navigation property
    public ICollection<DomainType> DomainTypes { get; set; } = new List<DomainType>();
}
