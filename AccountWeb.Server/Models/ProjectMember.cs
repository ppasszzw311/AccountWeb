namespace AccountWeb.Server.Models;

public class ProjectMember: BaseEntityWithTime
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public Project Project { get; set; } = null!;
    public User User { get; set; } = null!;
}
