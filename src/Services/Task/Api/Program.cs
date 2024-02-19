using Api.Configuration;
using Api.Middleware;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Application.Services;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
AddSwagger(services);

services.AddSingleton<IUnitOfWork, UnitOfWorkSqlServer>();
services.AddSingleton<ITaskService, TaskService>();

AddAutoMapper(services);

var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
services.AddIdentityServices(configuration.Build());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();

static void AddAutoMapper(IServiceCollection services)
{
    var assembliesToScan = new[]
    {
        Assembly.GetAssembly(typeof(WebApiMappingProfile))
    };

    services.AddAutoMapper(
        configAction =>
        {
            configAction.AllowNullCollections = true;
            configAction.AllowNullDestinationValues = true;
        },
        assembliesToScan);
}

static void AddSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "This API is responsible for task managing",

        });

        //c.OperationFilter<FileResultContentTypeOperationFilter>();

        //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"), true);
        //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Application.xml"), true);
    });
}