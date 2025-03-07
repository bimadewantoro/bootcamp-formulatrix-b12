using System.ComponentModel.DataAnnotations;

namespace DotnetApiPostgres.Api.Models.DTO;

public class LoginUserDTO
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}