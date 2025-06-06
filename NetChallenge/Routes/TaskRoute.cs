using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NetChallenge.Domain.Dto;
using NetChallenge.Extension.Helpers;
using NetChallenge.Services.Interface;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NetChallenge.Routes;

public static class TaskRoute
{
    public static void MapTaskRoute(this WebApplication app)
    {
        var tag = new List<OpenApiTag> { new() { Name = "Task" } };

        app.MapPost("/task",
       async (
          [FromServices] ITaskService service, [FromBody] CreateTaskDto create) =>
       {
           try
           {
               var created = await service.Create(create);
               return Results.Created();
           }
           catch (ProjectNotFoundException ex)
           {
               return Results.BadRequest(ex.Message);
           }
           catch (TasksExceedNumberTasksException ex)
           {
               return Results.BadRequest(ex.Message);
           }
       })
       .Produces((int)HttpStatusCode.Created)
       .WithName($"Create New Task")
       .WithOpenApi(x => new OpenApiOperation(x)
       {
           Summary = "Create new task",
           Description = "This endpoint creates a new task. It produces a 200 status code.",
           Tags = tag
       });

        app.MapPut("/task",
     async (
        [FromServices] ITaskService service, [FromBody] UpdateTaskDto update) =>
     {
         try
         {
             var updted = await service.Update(update);
             return Results.Ok();
         }
         catch (TasksNotFoundException ex)
         {
             return Results.BadRequest(ex.Message);
         }
     })
     .Produces((int)HttpStatusCode.OK)
        .WithName($"Update Task")
     .WithOpenApi(x => new OpenApiOperation(x)
     {
         Summary = "Update task",
         Description = "This endpoint update the task. It produces a 200 status code.",
         Tags = tag
     });

        app.MapDelete("/task",
    async (
         [FromServices] ITaskService service,[FromBody] RemoveTaskDto remove) =>
    {
        try
        {
            var updted = await service.Delete(remove);
            return Results.Ok();
        }
        catch (TasksNotFoundException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    })
    .Produces((int)HttpStatusCode.OK)
       .WithName($"Remove Task")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Remove task",
        Description = "This endpoint remove the task. It produces a 200 status code.",
        Tags = tag
    });


    }
}
