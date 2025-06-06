using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NetChallenge.Routes;

namespace NetChallenge.Extension;

public static class AppExtension
{
    public static void UseArchitectures(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "local")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseHealthChecks("/_health", new HealthCheckOptions() 
        { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.MapTaskRoute();
        app.MapProjectRoute();
        app.MapReportRoute();

    }
}
