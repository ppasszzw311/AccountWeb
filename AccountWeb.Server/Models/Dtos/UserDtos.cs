using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models.Dtos;

public class UserDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FactoryName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserDto(User user)
    {
        UserId = user.UserId;
        UserName = user.UserName;
        FactoryName = user.Factory?.FactoryName ?? string.Empty;
        Email = user.Email ?? string.Empty;
    }
}

public class CreateUserRequest
{
    [Required]
    public required string UserId { get; set; }
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
    public int? FactoryId { get; set; }
    [Required]
    public required string Email { get; set; }
}
