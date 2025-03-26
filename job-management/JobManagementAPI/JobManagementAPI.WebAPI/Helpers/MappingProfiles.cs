using AutoMapper;
using JobManagementAPI.WebAPI.Models.DTOs.Auth;
using JobManagementAPI.WebAPI.Models;

namespace JobManagementAPI.WebAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}