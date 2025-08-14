using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class DomainType: BaseEntityWithTime
{
    public int CategoryId { get; set; } // Foreign key to DomainCategory
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty; // Domain type name

    // Navigation property
    public DomainCategory Category { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}
