using NetChallenge.Domain.Entities;

namespace NetChallenge.Domain.Dto;

public record TaskDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTime CreatedDate { get; init; }
    public required int Id { get; init; }
    public required EnumPriorityTask Priority { get; init; }
    public ICollection<TaskLogDto>? TaskLogs { get; init; }
    public required string ProjectName { get; init; }
    public required string UserName { get; init; }
}
public record CreateTaskDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string CreatedUser { get; init; }
    public required EnumPriorityTask Priority { get; init; }
    public string? AdditionalComments { get; set; }
    public required string ProjectName { get; init; }
}
public record UpdateTaskDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? AdditionalComments { get; set; }
    public required EnumStatusTask Status { get; init; }
    public required string UserUpdate { get; init; }
}
public record RemoveTaskDto
{
   public required int Id { get; init; }
}

public class TaskLogDto
{
    public string? AdditionalComments { get; set; }
    public DateTime Created { get; set; } 
    public required string UserUpdate { get; set; }
    public required string UpdatedReport { get; set; }

}

public record TasksDoneByUserDto(string UserName, int NumberTasks, int TasksDone,decimal AverageDoneTask);
