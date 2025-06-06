using AutoMapper;
using NetChallenge.Domain.Dto;
using NetChallenge.Domain.Entities;
using NetChallenge.Extension.Helpers;
using NetChallenge.Repository.Interface;
using NetChallenge.Services.Interface;
using System.Text;

namespace NetChallenge.Services;

public class TaskService(ITaskRepository taskRepository,IProjectRepository projectrepository, IMapper mapper) : ITaskService
{
    
    
    public async Task<CreateTaskDto> Create(CreateTaskDto dto)
    {
        var project = await projectrepository.GetByProjectName(dto.ProjectName);
        if (project == null) {
            throw new ProjectNotFoundException();
        }
        if (project.Tasks?.Count() >= 20)
        {
            throw new TasksExceedNumberTasksException();
        }
        var newTask  = mapper.Map<Domain.Entities.Task>(dto);
        newTask.Project = project;

        await taskRepository.Create(newTask);
        return dto;
    }

    public async Task<bool> Delete(RemoveTaskDto dto)
    {
        var task = await  taskRepository.GetById(dto.Id);
        if (task == null)
        {
            throw new TasksNotFoundException();
        }
        return await taskRepository.Delete(task);

    }

    public async Task<IList<TasksDoneByUserDto>> TasksDoneByUser()
    {
        var tasks = await taskRepository.GetByPeriod(DateTime.Now.AddDays(-30));

        var totalTasks = tasks.GroupBy(info => info.UserName)
                        .Select(group => new {
                            UserName = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.UserName);

        var totalTasksDone = tasks.Where(r=>r.Status == EnumStatusTask.Done)
                    .GroupBy(info => info.UserName)
                      .Select(group => new {
                          UserName = group.Key,
                          Count = group.Count()
                      })
                      .OrderBy(x => x.UserName);

        var t = from o in totalTasks
                join i in totalTasksDone on o.UserName equals i.UserName
                into joinedT
                from pd in joinedT.DefaultIfEmpty()
                select new TasksDoneByUserDto(
                     o.UserName,o.Count,pd!=null? pd.Count:0, (decimal)(pd != null ? pd.Count : 0) /   o.Count 

                );
                

        return t.ToList();

    }

    public async Task<UpdateTaskDto> Update(UpdateTaskDto dto)
    {
        var task = await taskRepository.GetById(dto.Id);
        if (task == null)
        {
            throw new TasksNotFoundException();
        }
        var log = new TaskLog() { AdditionalComments = dto.AdditionalComments,
            Task = task, UserUpdate = dto.UserUpdate, UpdatedReport = reportLog(task,dto) } ;

        task.LastUpdate = DateTime.Now;
        task.Description = dto.Description;
        task.Name = dto.Name;
        task.Status = dto.Status;
        if(task.Logs ==null)
        {
            task.Logs = new List<TaskLog>();
        }
        task.Logs.Add(log);


       await taskRepository.Update(task);

        return dto;

    }

    private string reportLog(Domain.Entities.Task task, UpdateTaskDto dto)
    {
        StringBuilder sb = new StringBuilder();
        if(task.Status != dto.Status)
        {
            sb.Append($"Status alterado de {task.Status.ToString()} para {dto.Status.ToString()}");
            sb.AppendLine();
        }
        if (task.Name != dto.Name)
        {
            sb.Append($"Nome alterado de {task.Name} para {dto.Name}");
            sb.AppendLine();
        }
        if (task.Description != dto.Description)
        {
            sb.Append($"Descrição alterada de {task.Description} para {dto.Description}");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
