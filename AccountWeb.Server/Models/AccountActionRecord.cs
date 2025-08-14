using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;
public class AccountActionRecord: BaseEntity
{
    public int UserId { get; set; }
    public int AccountId { get; set; }
    [Required]
    [StringLength(20)]
    public string ActionType { get; set; } = string.Empty; // e.g., "Login", "Logout", "Update"

    [StringLength(1000)]
    public string? Details { get; set; } // Additional details about the action
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;
    public Account Account { get; set; } = null!;
}
