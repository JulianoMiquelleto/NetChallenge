namespace NetChallenge.Repository.Interface;

public interface IProjectRepository
{
    public Task<Domain.Entities.Project> Create(Domain.Entities.Project project);

    public Task<bool> Remove(Domain.Entities.Project project);

    public Task<Domain.Entities.Project> GetByProjectName(string projectName);

    public Task<Domain.Entities.Project> GetByProjectId(long id);

    public Task<IList<Domain.Entities.Project>> GetByUserName(string userName);
}
