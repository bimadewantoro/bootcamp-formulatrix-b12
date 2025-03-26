using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.WebAPI.Models.DTOs.Auth
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}