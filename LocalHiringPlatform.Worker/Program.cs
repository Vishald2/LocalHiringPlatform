using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure.Services;
using LocalHiringPlatform.ServiceBus.Extensions;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Messages;
using LocalHiringPlatform.ServiceBus.Services;
using LocalHiringPlatform.Worker;
using LocalHiringPlatform.Worker.Handlers;
using Resend;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ServiceBusWorker>();
builder.Services.AddServiceBus(builder.Configuration);

builder.Services.AddSingleton<IServiceBusConsumer,ServiceBusConsumer>();

builder.Services.AddScoped<
    IMessageHandler<EmailRequestModel>,
    EmailMessageHandler>();

builder.Services.AddScoped<IEmailService, ResendEmailService>();

builder.Services.AddHttpClient<ResendClient>();



var keyVaultUrl = builder.Configuration["KeyVault:Url"]!;

//var secretClient = new SecretClient(
//    new Uri(keyVaultUrl),
//    new DefaultAzureCredential());


TokenCredential credential;

if (builder.Environment.IsDevelopment())
{
    credential = new AzureCliCredential();
}
else
{
    credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
    {
        ExcludeManagedIdentityCredential = true
    });
}

var secretClient = new SecretClient(
    new Uri(keyVaultUrl),
    credential);



var resendApiKey = secretClient
    .GetSecretAsync("ResendApiKey")
    .GetAwaiter()
    .GetResult()
    .Value
    .Value;

Console.WriteLine(resendApiKey);

builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken = resendApiKey;
});

builder.Services.AddTransient<IResend, ResendClient>();

builder.Services.Configure<ResendSettings>(
    builder.Configuration.GetSection("Resend"));


builder.Services.AddWindowsService();
builder.Services.AddSingleton<IKeyVaultService, KeyVaultService>();

var host = builder.Build();
host.Run();
