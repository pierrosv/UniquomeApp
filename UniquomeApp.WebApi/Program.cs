using Microsoft.Extensions.Configuration;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using UniquomeApp.Application;
using UniquomeApp.EfCore;
using UniquomeApp.Infrastructure.Security;
using UniquomeApp.SharedKernel;
using UniquomeApp.WebApi.Extensions;
using UniquomeApp.WebApi.Filters;
using UniquomeApp.WebApi.Models;
using UniquomeApp.WebApi.Options;
using UniquomeApp.WebApi.Services;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(opts =>
{
    opts.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(120);
    opts.Limits.MaxResponseBufferSize = long.MaxValue;
});

var dbConnections = new List<DbConnectionOptions>();
config.GetSection("DbConnections").Bind(dbConnections);
if (!dbConnections.Any()) throw new Exception("Unable to get DB Connections");

var apiOptions = new ApiOptions();
config.GetSection(nameof(ApiOptions)).Bind(apiOptions);

var apiMessages = new List<ApiMessage>();
config.GetSection("ApiMessages").Bind(apiMessages);
builder.Services.AddSingleton(apiMessages);

var emailContexts = new List<EmailContext>();
config.GetSection("EmailContexts").Bind(emailContexts);
builder.Services.AddSingleton(emailContexts);
var emailSettings = new EmailSettings();
config.GetSection(nameof(EmailSettings)).Bind(emailSettings);
builder.Services.AddSingleton(emailSettings);

builder.Services.AddScoped<IApiMessageService, ApiMessageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ApiOptions>(apiOptions);
builder.Services.AddHttpContextAccessor();
builder.Services.AddTokenAuthentication(config);
builder.Services.AddApplication();
builder.Services.AddPersistence(config);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddControllers(x => x.Filters.Add<ApiExceptionFilterAttribute>())
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        opts.JsonSerializerOptions.Converters.Add(new IntToStringConverter());
        opts.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
        opts.JsonSerializerOptions.Converters.Add(new DecimalIntToStringConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseFluentValidationExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
