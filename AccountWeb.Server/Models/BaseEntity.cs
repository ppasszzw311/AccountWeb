using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

/// <summary>
/// 基礎的物件類別
/// </summary>
public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
