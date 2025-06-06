using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetChallenge.Domain.Mapping;
using NetChallenge.Repository;
using NetChallenge.Repository.Interface;
using NetChallenge.Services;
using NetChallenge.Services.Interface;

namespace NetChallenge.Extension;

public static class BuilderExtension
{
    public static void AddArchitectures(this WebApplicationBuilder builder)
    {
        builder.BuildConfiguration();
        var config = builder.Configuration as IConfiguration;

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();
        builder.Services.AddHealthChecks();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // Logging
        builder.Logging.SetMinimumLevel(LogLevel.Error).AddConsole();

        builder
            .AddSwagger();

        //AutoMapper
        builder.Services
                  .AddAutoMapper(typeof(TaskMapping))
                 .AddAutoMapper(typeof(ProjectMapping));

        //DI
        builder.InjectServiceDependencies(config);

        //In MemoryDatabase
        builder.Services.AddDbContext<NetChallenge.Repository.Context.NetChallengeContext>(opt =>opt.UseInMemoryDatabase("TasksDb"));
    }

    private static WebApplicationBuilder BuildConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
                    .SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

        return builder;
    }

    private static void InjectServiceDependencies(this WebApplicationBuilder builder, IConfiguration config)
    {
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();

        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    }
    private static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "NetChallenge.Api",
            });
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                   new string[] {}
                }
            });
        });

        return builder;
    }
}
