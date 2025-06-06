using System.ComponentModel.DataAnnotations;

namespace NetChallenge.Domain.Entities;

public class Task
{
    [Key]
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? LastUpdate { get; set; }
    public EnumPriorityTask Priority { get; set; }
    public ICollection<TaskLog>? Logs { get; set; }
    public required Project Project { get; set; }
    public required EnumStatusTask Status { get; set; } = EnumStatusTask.ToDo;
    public required string UserName { get; set; }
}
