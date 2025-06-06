using Microsoft.EntityFrameworkCore;
using NetChallenge.Domain.Entities;
using NetChallenge.Repository.Context;
using NetChallenge.Repository.Interface;

namespace NetChallenge.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly NetChallengeContext _context;

    public TaskRepository(NetChallengeContext context)
    {
        _context = context;
    }
    public async Task<Domain.Entities.Task> Create(Domain.Entities.Task task)
    {
        var addTask = await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return addTask.Entity;
    }

    public async Task<bool> Delete(Domain.Entities.Task task)
    {
        _context.Tasks.Remove(task);
        return _context.SaveChanges() > -1;
    }

    public async Task<Domain.Entities.Task> GetById(long id)
    {
        return await _context.Tasks.Include(r => r.Logs).FirstOrDefaultAsync(r=>r.Id ==id);
    }

    public async Task<IList<Domain.Entities.Task>> GetByPeriod(DateTime createdDate)
    {
        return await _context.Tasks.Where(r => r.Created > createdDate).ToListAsync();
    }

    public async Task<IList<Domain.Entities.Task>> GetByProject(string projectName)
    {
        return await _context.Tasks.Include(r => r.Project).Where(r => r.Project.Name == projectName).ToListAsync();
    }

    public async Task<Domain.Entities.Task> Update(Domain.Entities.Task task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }
}
