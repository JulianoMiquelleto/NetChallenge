using Microsoft.EntityFrameworkCore;
using NetChallenge.Domain.Entities;
using NetChallenge.Repository.Context;
using NetChallenge.Repository.Interface;
using System.Xml.Linq;

namespace NetChallenge.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly NetChallengeContext _context;

    public ProjectRepository(NetChallengeContext context)
    {
        _context = context;
    }
    
    public async Task<Project> Create(Project project)
    {
        var addproject = await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return addproject.Entity;
    }

    public async Task<IList<Project>> GetByUserName(string userName)
    {
        return await _context.Projects.Where(r=> string.Equals(userName, r.CreatedUser, StringComparison.OrdinalIgnoreCase)).ToListAsync();
    }

    public async Task<Project?> GetByProjectName(string projectName)
    {
        return await _context.Projects.Include(r=>r.Tasks).ThenInclude(f=>f.Logs).FirstOrDefaultAsync(r => string.Equals(projectName, r.Name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<bool> Remove(Project project)
    {
        _context.Projects.Remove(project);
        return await _context.SaveChangesAsync() > -1;
    }

    public async Task<Project> GetByProjectId(long id)
    {
        return await _context.Projects.Include(r=>r.Tasks).FirstOrDefaultAsync(r=>r.Id ==id);
    }
}
