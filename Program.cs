using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Text("LocalHiringPlatform API running"));

app.Run();
/*
 * This is the entry point of the LocalHiringPlatform API application. It sets up a simple web server using ASP.NET Core.
 * The application responds to GET requests at the root URL ("/") with a plain text message indicating that the API is running.
 * The application is configured to run on .NET 10.0, as specified in the project file.
 */