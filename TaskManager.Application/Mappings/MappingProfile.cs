using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WorkItem, WorkItemDto>().ReverseMap();
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.RoleName, 
                      opt => opt.MapFrom(src => src.Role.Name));
    }
} 