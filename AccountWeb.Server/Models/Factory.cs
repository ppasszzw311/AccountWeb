using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models;

public class Factory : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string FactoryId { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string FactoryName { get; set; } = string.Empty;

}
