namespace JobManagementAPI.WebAPI.Models.DTOs.Job
{
    public class CreateJobDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public string JobType { get; set; }

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public bool IsActive { get; set; } = true;
    }
}