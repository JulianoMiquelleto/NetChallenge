using NetChallenge.Domain.Dto;

namespace NetChallenge.Services.Interface;

public interface IProjectService
{
    Task<CreateProjectDto> Create(CreateProjectDto dto);

    Task<bool> Remove(long idProject);

    Task<IList<TaskDto>> DisplayTasksByProject(string projectName);

    Task<IList<ProjectDto>> DisplayProjectsByUser(string userName);
}
