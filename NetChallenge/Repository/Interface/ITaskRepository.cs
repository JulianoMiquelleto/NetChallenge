namespace NetChallenge.Repository.Interface;

public interface ITaskRepository
{
    public Task<Domain.Entities.Task> Create(Domain.Entities.Task task);

    public Task<Domain.Entities.Task> Update(Domain.Entities.Task task);

    public Task<Domain.Entities.Task> GetById(long idTask);

    public Task<IList<Domain.Entities.Task>> GetByProject(string projectName);

    public Task<bool> Delete(Domain.Entities.Task task);

    public Task<IList<Domain.Entities.Task>> GetByPeriod(DateTime createdDate);
}
