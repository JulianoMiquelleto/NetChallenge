using Microsoft.EntityFrameworkCore;
using NetChallenge.Domain.Entities;

namespace NetChallenge.Repository.Context;

public class NetChallengeContext:DbContext
{
    public NetChallengeContext(DbContextOptions<NetChallengeContext> options):base(options)
    {

    }
    public DbSet<Project> Projects { get; set; }

    public DbSet<Domain.Entities.Task> Tasks { get; set; }
}
