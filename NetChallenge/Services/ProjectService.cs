using AutoMapper;
using NetChallenge.Domain.Dto;
using NetChallenge.Domain.Entities;
using NetChallenge.Extension.Helpers;
using NetChallenge.Repository.Interface;
using NetChallenge.Services.Interface;
using System.Collections.Generic;

namespace NetChallenge.Services;

public sealed class ProjectService(IProjectRepository projectRepository, IMapper mapper) : IProjectService
{
    
    public async Task<CreateProjectDto> Create(CreateProjectDto dto)
    {
        try
        {
            var newProject = mapper.Map<Project>(dto);
            await projectRepository.Create(newProject);
            return dto;
        }
        catch (Exception) {
            throw new ProjectCreateException();
        }
    }

    public async Task<IList<ProjectDto>> DisplayProjectsByUser(string userName)
    {
        var list = await projectRepository.GetByUserName(userName);
        IList<ProjectDto> projects = mapper.Map<IList<Project>, IList<ProjectDto>>(list);
        return projects;
    }

    public async Task<IList<TaskDto>> DisplayTasksByProject(string projectName)
    {
        var project = await projectRepository.GetByProjectName(projectName);
        IList<TaskDto> tasks = mapper.Map<IList<Domain.Entities.Task>, IList<TaskDto>>(project?.Tasks?.ToList());

        return tasks;

    }

    public async Task<bool> Remove(long idProject)
    {
        var project = await projectRepository.GetByProjectId(idProject);
        if (project == null) {
            throw new ProjectNotFoundException();
        }
        if (project.Tasks?.Count > 0 && project.Tasks.Where(r=>r.Status == EnumStatusTask.ToDo).Count() > 0) 
        {
            throw new ProjectTaskException();
        }
        return await projectRepository.Remove(project);
    }
}
