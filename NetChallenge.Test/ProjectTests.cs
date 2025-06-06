using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetChallenge.Domain.Mapping;
using NetChallenge.Extension.Helpers;
using NetChallenge.Repository;
using NetChallenge.Repository.Context;
using NetChallenge.Services;

namespace NetChallenge.Test
{
    public class ProjectTests
    {
        private readonly ProjectService _service;
        private readonly ProjectRepository _repository;
        public ProjectTests() 
        {

            var dbOptionsBuilder = new DbContextOptionsBuilder<NetChallengeContext>().UseInMemoryDatabase("testeDb");
            _repository = new ProjectRepository(new NetChallengeContext(dbOptionsBuilder.Options));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            _service = new ProjectService(_repository, mapper);
            
        }

        /// <summary>
        /// Teste em caso de tentar remover projeto e o mesmo não existir
        /// </summary>
        [Fact]
        public void Project_ShouldThrowNotFoundException_WhenRemove()
        {        
            Assert.ThrowsAsync<ProjectNotFoundException>(() => _service.Remove(1));
        }

        /// <summary>
        /// Teste em caso de tentar remover projeto e o ver tasks pendentes
        /// </summary>
        [Fact]
        public async Task Project_ShouldThrowNotFoundException_WhenRemoveAndExistsTasks()
        {
            var tasks = new List<Domain.Entities.Task>();
            var project = new Domain.Entities.Project() { CreatedUser = "UserTest", Name = "ProjectTest" };

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(new Domain.Entities.Task() { Name = "Task1"+i, Description = "Description", 
                    Project = project, Status = Domain.Entities.EnumStatusTask.ToDo,UserName = "juca" });
            }
            project.Tasks = tasks;

           var projectAdd = await _repository.Create(project);
            
           await Assert.ThrowsAsync<ProjectTaskException>(() => _service.Remove(project.Id));
        }
    }
}