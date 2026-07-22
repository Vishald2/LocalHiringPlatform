using LocalHiringPlatform.Api.Middleware;
using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Helpers;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Interfaces.AI.LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Interfaces.Experience;
using LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories;
using LocalHiringPlatform.Domain.Interfaces.MasterDataServices;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure;
using LocalHiringPlatform.Infrastructure.Data;
using LocalHiringPlatform.Infrastructure.Repositories;
using LocalHiringPlatform.Infrastructure.Repositories.EducationRepositories;
using LocalHiringPlatform.Infrastructure.Repositories.Experience;
using LocalHiringPlatform.Infrastructure.Repositories.MasterData;
using LocalHiringPlatform.Infrastructure.Services;
using LocalHiringPlatform.Infrastructure.Services.AI;
using LocalHiringPlatform.Infrastructure.Services.AI.IntentHandlers;
using LocalHiringPlatform.Infrastructure.Services.AI.IntentHandlers.LocalHiringPlatform.Infrastructure.Services.AI.IntentHandlers;
using LocalHiringPlatform.Infrastructure.Services.Education;
using LocalHiringPlatform.Infrastructure.Services.Experience;
using LocalHiringPlatform.Infrastructure.Services.MasterData;
using LocalHiringPlatform.ServiceBus.Extensions;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Resend;
using Serilog;
using StackExchange.Redis;
using System.Text;
using Log = Serilog.Log;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection("Gemini"));

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "LocalHiringPlatform API",
            Version = "v1"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter JWT token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddScoped<
    IUserRepository,
    UserRepository>();

builder.Services.AddScoped<
    ICandidateProfileRepository,
    CandidateProfileRepository>();

builder.Services.AddScoped<IEmployerProfileRepository,
    EmployerProfileRepository>();

builder.Services.AddScoped<IUnitOfWork,
    UnitOfWork>();

builder.Services.AddScoped<IAuthService,
    AuthService>();

builder.Services.AddScoped<ICandidateProfileService,
    CandidateProfileService>();

builder.Services.AddScoped<ISkillService,
    SkillService>();

builder.Services.AddScoped<ISkillRepository,
    SkillRepository>();

builder.Services.AddScoped<
    IJobRepository,
    JobRepository>();

builder.Services.AddScoped<
    IJobService,
    JobService>();

builder.Services.AddScoped<
    IJobApplicationRepository,
    JobApplicationRepository>();

builder.Services.AddScoped<
    IJobApplicationService,
    JobApplicationService>();

builder.Services.AddScoped<
    IEmployerDashboardService,
    EmployerDashboardService>();
 
builder.Services.AddScoped<
    ICandidateDashboardService,
    CandidateDashboardService>();

builder.Services.AddScoped<
    ICandidateSearchService,
    CandidateSearchService>();

builder.Services.AddScoped<
    INotificationRepository,
    NotificationRepository>();

builder.Services.AddScoped<
    INotificationService,
    NotificationService>();

builder.Services.AddScoped<
    ISavedJobRepository,
    SavedJobRepository>();

builder.Services.AddScoped<
    ISavedJobService,
    SavedJobService>();

builder.Services.AddScoped<
    ICandidateSkillRepository,
    CandidateSkillRepository>();

builder.Services.AddScoped<
    ICandidateSkillService,
    CandidateSkillService>();

builder.Services.AddScoped<
    IMobileVerificationService,
    MobileVerificationService>();

builder.Services.AddScoped<
    IAiMatchingService,
    AiMatchingService>();

builder.Services.AddScoped<
        IAiAnalysisRepository,
        AiAnalysisRepository>();

builder.Services.AddScoped<IAIChatService, AIChatService>();

builder.Services.AddHttpClient<ILLMService, GeminiLLMService>();

builder.Services.AddScoped<IPromptService, PromptService>();

builder.Services.AddScoped<ILLMService, GeminiLLMService>();

builder.Services.AddScoped<
    IAIIntentHandler,
    GreetingIntentHandler>();

builder.Services.AddScoped<
    IAIIntentHandler,
    JobSearchIntentHandler>();

builder.Services.AddServiceBus(builder.Configuration);

builder.Services.AddSingleton<IServiceBusPublisher, ServiceBusPublisher>();

builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();

builder.Services.Configure<ResendSettings>(
    builder.Configuration.GetSection("Resend"));

builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection("Application"));

builder.Services.Configure<Msg91Settings>(
    builder.Configuration.GetSection("Msg91"));

builder.Services.AddHttpClient<Msg91Helper>();

builder.Services.AddHttpClient<ISmsService, Msg91SmsService>(
    (serviceProvider, client) =>
    {
        var settings = serviceProvider
            .GetRequiredService<IOptions<Msg91Settings>>()
            .Value;

        client.BaseAddress = new Uri(settings.BaseUrl);
    });

builder.Services.AddScoped<IEmailService,
    ResendEmailService>();

builder.Services.AddHttpClient<IEmailService, ResendEmailService>();

builder.Services.AddOptions();

builder.Services.AddTransient<IEmailService, ResendEmailService>();

/*REGISTER RESEND SDK*/
builder.Services.AddHttpClient<ResendClient>();

builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken =
        builder.Configuration["Resend:ApiKey"]!;
});

builder.Services.AddTransient<IResend, ResendClient>();

//builder.Services.AddInfrastructure(builder.Configuration);

/*EDUCATION. START*/
builder.Services.AddScoped<
    IEducationRepository,
    EducationRepository>();
builder.Services.AddScoped<ICandidateEducationRepository, CandidateEducationRepository>();

builder.Services.AddScoped<IEducationRepository, EducationRepository>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ICourseSpecializationRepository, CourseSpecializationRepository>();

builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();

builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

builder.Services.AddScoped<ICandidateEducationSpecializationRepository, CandidateEducationSpecializationRepository>();


builder.Services.AddScoped<
    ICourseRepository,
    CourseRepository>();



builder.Services.AddScoped<
    ICourseSpecializationRepository,
    CourseSpecializationRepository>();

builder.Services.AddScoped<
    IUniversityRepository,
    UniversityRepository>();

//SERVICES
builder.Services.AddScoped<
    IUniversityService,
    UniversityService>();

builder.Services.AddScoped<
    ICourseSpecializationService,
    CourseSpecializationService>();

builder.Services.AddScoped<
    ICourseService,
    CourseService>();
builder.Services.AddScoped<
    IEducationService,
    EducationService>();
builder.Services.AddScoped<ICandidateEducationService, CandidateEducationService>();
/*EDUCATION. END*/

/*Candidate Experience. Start*/
builder.Services.AddScoped<ICandidateExperienceRepository,
    CandidateExperienceRepository>();

//builder.Services.AddScoped<ICandidateExperienceDetailRepository,
//    CandidateExperienceDetailRepository>();

builder.Services.AddScoped<ICandidateExperienceService,
    CandidateExperienceService>();

builder.Services.AddScoped<IIndustryTypeService,
    IndustryTypeService>();

builder.Services.AddScoped<IIndustryTypeRepository,
    IndustryTypeRepository>();
/*Experience. End*/

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "ReactPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

/*JWT Work*/
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

var jwtSection =
    builder.Configuration.GetSection("Jwt");

var key =
    Encoding.UTF8.GetBytes(
        jwtSection["Key"]!);

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    jwtSection["Issuer"],

                ValidAudience =
                    jwtSection["Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(key)
            };
    });
/*JWT Work. End*/

builder.Services.AddScoped<
    IJwtTokenService,
    JwtTokenService>();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration =
        builder.Configuration
            .GetValue<string>("Redis:ConnectionString");

    return ConnectionMultiplexer.Connect(
        configuration!);
});

builder.Services.AddScoped<
    IRedisCacheService,
    RedisCacheService>();

Log.Information(
    "LocalHire API Started");

var app = builder.Build();

var useRedis =
    builder.Configuration.GetValue<bool>(
        "Application:UseRedis");

Console.WriteLine(useRedis);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("ReactPolicy");

app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();



app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/version", () => "2026-07-22");

app.Run();
