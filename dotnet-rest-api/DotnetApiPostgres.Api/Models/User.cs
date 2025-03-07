using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApiPostgres.Api.Models;

[Table("User")]
public class User
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    [Required]
    public required string Username { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }
}