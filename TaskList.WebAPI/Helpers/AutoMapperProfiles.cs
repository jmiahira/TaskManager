using AutoMapper;
using TaskList.Domain;
using TaskList.WebAPI.DTO;

namespace TaskList.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Tasks, TasksDTO>().ReverseMap();
            CreateMap<TaskRemarks, TaskRemarksDTO>().ReverseMap();
        }
    }
}