using AutoMapper;
using JobManagementAPI.WebAPI.Models.DTOs.Auth;
using JobManagementAPI.WebAPI.Models.DTOs.Job;
using JobManagementAPI.WebAPI.Models;

namespace JobManagementAPI.WebAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();
            
            CreateMap<CreateJobDto, Job>();
            CreateMap<UpdateJobDto, Job>();
            CreateMap<Job, JobDto>();
        }
    }
}