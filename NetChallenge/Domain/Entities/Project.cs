using System.ComponentModel.DataAnnotations;

namespace NetChallenge.Domain.Entities;

public class Project
{
    [Key]
    public long Id { get; set; }
    public required string Name { get; set; }
    public  string? Description { get; set; }
    public string? CreatedUser { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Task>? Tasks { get; set; }

}
