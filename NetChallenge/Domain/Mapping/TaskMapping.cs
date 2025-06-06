using AutoMapper;
using NetChallenge.Domain.Dto;

namespace NetChallenge.Domain.Mapping;

public class TaskMapping : Profile
{
    public TaskMapping()
    {
        CreateMap<CreateTaskDto, Entities.Task>()
             .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
             .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
             .ForMember(dest => dest.Priority, o => o.MapFrom(src => src.Priority))
             .ForMember(dest => dest.UserName, o => o.MapFrom(src => src.CreatedUser))
             ;

        CreateMap<Entities.Task, TaskDto>()
            .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedDate, o => o.MapFrom(src => src.Created))
            .ForMember(dest => dest.ProjectName, o => o.MapFrom(src => src.Project.Name))
            .ForMember(dest => dest.TaskLogs, o => o.MapFrom(src => src.Logs))
            .ForMember(dest => dest.UserName, o => o.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description));


        CreateMap<Entities.TaskLog, TaskLogDto>()
           .ForMember(dest => dest.Created, o => o.MapFrom(src => src.Created))
           .ForMember(dest => dest.UpdatedReport, o => o.MapFrom(src => src.UpdatedReport))
           .ForMember(dest => dest.UserUpdate, o => o.MapFrom(src => src.UserUpdate))
           .ForMember(dest => dest.AdditionalComments, o => o.MapFrom(src => src.AdditionalComments));
           
    }
}
