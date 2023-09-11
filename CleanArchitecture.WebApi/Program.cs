using CleanArchitecture.WebApi.Configurations;
using CleanArchitecture.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices( //configuration içinde IInstallServices'i implement eden classları alır ve içlerindekileri Program.cs'e register eder.
    builder.Configuration,
    builder.Host,
    typeof(IServiceInstaller).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // default cors policy uyuglandığından polcy için isim vermeye gerek yok

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
