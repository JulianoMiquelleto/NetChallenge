namespace NetChallenge.Domain.Dto;

public record ProjectDto
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required string CreatedUser { get; init; }
    public required DateTime CreatedDate { get; init; }
    public ICollection<TaskDto>? Tasks { get; init; }

    public required long Id { get; set; }
}
public record CreateProjectDto {
    public required string Name { get; init; }
    public required string CreatedUser { get; init; }
    public string? Description { get; init; }
}
public record UpdateProjectDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}
public record RemoveProjectDto
{
    public required int Id { get; init; }
}