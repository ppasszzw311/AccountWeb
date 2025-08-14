using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class LoginRecord: BaseEntity
{
    [Required]
    public int UserId { get; set; }
    public DateTime LoginTime { get; set; } = DateTime.UtcNow;
    public DateTime? LogoutTime { get; set; }
    public TimeSpan? Duration
    {
        get
        {
            if (LogoutTime != null)
            {
                return LogoutTime.Value - LoginTime;
            }
            return null;
        }
    }
    [StringLength(45)]
    public string? IpAddress { get; set; } // IP address of the user

    // Navigation properties
    public User User { get; set; } = null!;
}
