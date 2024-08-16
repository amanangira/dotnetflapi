using AutoMapper;
using flashlightapi.DTOs;
using flashlightapi.DTOs.assignment;
using flashlightapi.Models;

namespace flashlightapi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<User, UserDTO>();
        // CreateMap<UserDTO, User>();
        CreateMap<AppUser, AccountDTO>().
            ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).
            ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName)).
            ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        CreateMap<Assignment, AssignmentDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.CloseAt, opt => opt.MapFrom(src => src.CloseAt))
            .ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => src.StartAt));
        // ForMember(dest => dest.CreatedById), opt => opt.MapFrom(src => src.CreatedById));
        // ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.));

        // CreateMap<List<Assignment>, List<AssignmentDTO>>();
        // CreateMap<AssignmentDTO, Assignment>();
    }
}