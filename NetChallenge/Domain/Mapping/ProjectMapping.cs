using AutoMapper;
using NetChallenge.Domain.Dto;

namespace NetChallenge.Domain.Mapping;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<CreateProjectDto, Entities.Project>()
             .ForMember(dest => dest.CreatedUser, o => o.MapFrom(src => src.CreatedUser))
             .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
             .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description));

        CreateMap< Entities.Project,ProjectDto>()
            .ForMember(dest => dest.CreatedUser, o => o.MapFrom(src => src.CreatedUser))
            .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedDate, o => o.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description));
    }
}
