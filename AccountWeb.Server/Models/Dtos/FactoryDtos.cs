using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Server.Models.Dtos;


public class CreateFactoryRequest
{
    [Required]
    public string FactoryId { get; set; } = string.Empty;

    [Required]
    public string FactoryName { get; set; } = string.Empty;
}

public class UpdateFactoryRequest
{
    [Required]
    public string FactoryId { get; set; } = string.Empty;

    [Required]
    public string FactoryName { get; set; } = string.Empty;
}
