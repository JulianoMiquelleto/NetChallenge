using NetChallenge.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.AddArchitectures();
var app = builder.Build();

app.UseArchitectures();

app.Run();
