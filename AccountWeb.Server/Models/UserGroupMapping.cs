namespace AccountWeb.Server.Models;
public class UserGroupMapping : BaseEntityWithTime
{
    public int UserId { get; set; }
    public int GroupId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Group Group { get; set; } = null!;
}
