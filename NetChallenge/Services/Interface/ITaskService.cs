using NetChallenge.Domain.Dto;

namespace NetChallenge.Services.Interface;

public interface ITaskService
{
    Task<CreateTaskDto> Create(CreateTaskDto dto);

    Task<UpdateTaskDto> Update(UpdateTaskDto dto);

    Task<bool> Delete(RemoveTaskDto dto);
    Task<IList<TasksDoneByUserDto>> TasksDoneByUser();
}
