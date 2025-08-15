using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models.Dtos;

public class CreateGroupRequest
{
    [Required]
    public string GroupName { get; set; } = string.Empty;

    [Required]
    public int ProjectId { get; set; }

    public string? Description { get; set; }
}

public class UpdateGroupRequest
{
    [Required]
    public string GroupName { get; set; } = string.Empty;

    [Required]
    public int ProjectId { get; set; }

    public string? Description { get; set; }
}

public class AddUserToGroupRequest
{
    [Required]
    public int UserId { get; set; }
}