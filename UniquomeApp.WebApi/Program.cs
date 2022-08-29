using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using UniquomeApp.Application;
using UniquomeApp.EfCore;
using UniquomeApp.SharedKernel;
using UniquomeApp.WebApi.Extensions;
using UniquomeApp.WebApi.Filters;

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


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPersistence(config);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
