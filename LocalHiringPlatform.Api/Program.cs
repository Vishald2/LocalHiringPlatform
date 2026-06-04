using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure;
using LocalHiringPlatform.Infrastructure.Data;
using LocalHiringPlatform.Infrastructure.Repositories;
using LocalHiringPlatform.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<
    IUserRepository,
    UserRepository>();

builder.Services.AddScoped<
    ICandidateProfileRepository,
    CandidateProfileRepository>();

builder.Services.AddScoped<
    IUnitOfWork,
    UnitOfWork>();

builder.Services.AddScoped<
    IAuthService,

    AuthService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
