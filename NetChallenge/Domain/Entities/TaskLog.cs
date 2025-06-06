namespace NetChallenge.Domain.Entities;

public class TaskLog
{
    public long Id { get; set; }
    public  string? AdditionalComments { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public required string UserUpdate { get; set; }
    public required Task Task { get; set; }
    public required string UpdatedReport { get; set; }

}
