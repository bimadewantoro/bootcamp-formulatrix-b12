using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.WebAPI.Models.DTOs.Auth
{
    public class LogoutDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}