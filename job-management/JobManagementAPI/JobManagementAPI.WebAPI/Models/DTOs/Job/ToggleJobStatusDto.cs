using System.ComponentModel.DataAnnotations;

namespace JobManagementAPI.WebAPI.Models.DTOs.Job
{
    public class ToggleJobStatusDto
    {
        [Required]
        public bool IsActive { get; set; }
    }
}