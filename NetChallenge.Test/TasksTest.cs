using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetChallenge.Repository.Context;
using NetChallenge.Repository;
using NetChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetChallenge.Domain.Mapping;
using NetChallenge.Extension.Helpers;
using NetChallenge.Domain.Dto;
using NetChallenge.Domain.Entities;

namespace NetChallenge.Test
{
    public  class TasksTest
    {
        private readonly TaskService _service;
        private readonly TaskRepository _repository;
        private readonly ProjectRepository _repositoryProject;
        public TasksTest()
        {

            var dbOptionsBuilder = new DbContextOptionsBuilder<NetChallengeContext>().UseInMemoryDatabase("testeDb");
            _repository = new TaskRepository(new NetChallengeContext(dbOptionsBuilder.Options));
            _repositoryProject = new ProjectRepository(new NetChallengeContext(dbOptionsBuilder.Options));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TaskMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            _service = new TaskService(_repository, _repositoryProject, mapper);

        }

        [Fact]
        public void Task_ShouldThrowNotFoundProjectException_WhenCreate()
        {
            var dto = new CreateTaskDto() { ProjectName="ProjectTest",CreatedUser="Test",Description="Test",Name="TestName",Priority = Domain.Entities.EnumPriorityTask.Low};
            Assert.ThrowsAsync<ProjectNotFoundException>(() => _service.Create(dto));
        }

        [Fact]
        public async void Task_ShouldThrowTaskNumberExceeed_WhenCreate()
        {
            var tasks = new List<Domain.Entities.Task>();
            var dto = new CreateTaskDto() { ProjectName = "ProjectTest", CreatedUser = "Test", Description = "Test", Name = "TestName",
                Priority = Domain.Entities.EnumPriorityTask.Low };

            var project = new Domain.Entities.Project() { CreatedUser = dto.CreatedUser, Name = dto.ProjectName };

            for (int i = 0; i < 20; i++)
            {
                tasks.Add(new Domain.Entities.Task() { Name = "Task1" + i, Description = "Description", 
                    Project = project, Status = Domain.Entities.EnumStatusTask.ToDo,UserName="Juca" });
            }
            project.Tasks = tasks;

            var projectAdd = await _repositoryProject.Create(project);


           await  Assert.ThrowsAsync<TasksExceedNumberTasksException>(() => _service.Create(dto));
        }
    }
}
