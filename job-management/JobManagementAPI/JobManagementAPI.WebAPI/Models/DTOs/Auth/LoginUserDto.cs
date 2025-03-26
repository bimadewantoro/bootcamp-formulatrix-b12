using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.WebAPI.Models.DTOs.Auth
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}