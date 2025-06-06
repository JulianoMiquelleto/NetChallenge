using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NetChallenge.Domain.Dto;
using NetChallenge.Extension.Helpers;
using NetChallenge.Services.Interface;
using System.Net;

namespace NetChallenge.Routes;

public static class ProjectRoute
{
    public static void MapProjectRoute(this WebApplication app)
    {
        var tag = new List<OpenApiTag> { new() { Name = "Project" } };

        app.MapPost("/project",
       async (
          [FromServices] IProjectService service, [FromBody] CreateProjectDto create) =>
       {
           try
           {
               var created = await service.Create(create);
               return Results.Created();
           }
           catch(ProjectCreateException ex)
           {
                return Results.BadRequest(ex.Message);
           }
       })
       .Produces((int)HttpStatusCode.Created)
       .WithName($"Create New Project")
       .WithOpenApi(x => new OpenApiOperation(x)
       {
           Summary = "Create new project",
           Description = "This endpoint creates a new project. It produces a 200 status code.",
           Tags = tag
       });

        app.MapDelete("/project/{projectId}",
       async (
          [FromServices] IProjectService service, long projectId) =>
       {
           try
           {
               var deleted = await service.Remove(projectId);
               return Results.Ok();
           }
           catch (ProjectNotFoundException ex)
           {
               return Results.BadRequest(ex.Message);
           }
           catch (ProjectTaskException ex)
           {
               return Results.BadRequest(ex.Message);
           }
       })
       .Produces((int)HttpStatusCode.OK)
       .WithName($"Remove Project")
       .WithOpenApi(x => new OpenApiOperation(x)
       {
           Summary = "Remove project",
           Description = "This endpoint removes a project. It produces a 200 status code.",
           Tags = tag
       });

        app.MapGet("/project/DisplayProjectsByUser/{userName}",
     async (
        [FromServices] IProjectService service, string userName) =>
     {
         var projects = await service.DisplayProjectsByUser(userName);
         return Results.Ok(projects);
     })
     .Produces((int)HttpStatusCode.OK)
     .WithName($"Display Projects By user")
     .WithOpenApi(x => new OpenApiOperation(x)
     {
         Summary = "Display Projects By user",
         Description = "This endpoint display projects by user. It produces a 200 status code.",
         Tags = tag
     });

        app.MapGet("/project/DisplayTasksByProject/{projectName}",
    async (
       [FromServices] IProjectService service, string projectName) =>
    {
        var tasks = await service.DisplayTasksByProject(projectName);
        return Results.Ok(tasks);
    })
    .Produces((int)HttpStatusCode.OK)
    
    .WithName($"Display Tasks By Project")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Display tasks by project",
        Description = "This endpoint display tasks by project name. It produces a 200 status code.",
        Tags = tag
    });


    }


}
