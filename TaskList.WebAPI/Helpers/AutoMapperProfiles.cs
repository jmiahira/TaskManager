using AutoMapper;
using TaskList.Domain;
using TaskList.Domain.Identity;
using TaskList.WebAPI.DTO;

namespace TaskList.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Tasks, TasksDTO>().ReverseMap();
            CreateMap<TaskRemarks, TaskRemarksDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}