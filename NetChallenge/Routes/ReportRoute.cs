using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NetChallenge.Services.Interface;
using System.Net;

namespace NetChallenge.Routes;

public static class ReportRoute
{
    public static void MapReportRoute(this WebApplication app)
    {
        var tag = new List<OpenApiTag> { new() { Name = "Report" } };

              app.MapGet("/report/TasksDone",
     async (
        [FromServices] ITaskService service,
        [Microsoft.AspNetCore.Mvc.FromServices] IHttpContextAccessor http) =>
     {

         var authRole = http.HttpContext?.Request?.Headers.Authorization;
         if(authRole is null || authRole.Value != "Manager")
         {
             return Results.Unauthorized();
         }

         var tasks = await service.TasksDoneByUser();
         return Results.Ok(tasks);
     })
     .Produces((int)HttpStatusCode.OK)
     .WithName($"Display Tasks done by user, average from last 30 days")
     .WithOpenApi(x => new OpenApiOperation(x)
     {
         Summary = "Display Tasks done by user, average from last 30 days",
         Description = "This endpoint display number of tasks done by user. It produces a 200 status code.",
         Tags = tag
     });


    }


}
