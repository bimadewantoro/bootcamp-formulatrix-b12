namespace DotnetApiPostgres.Api.Models.DTO;

public class UserResponseDTO
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}