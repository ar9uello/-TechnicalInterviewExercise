using Api.Configuration;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Application.Services;
using Persistence.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<IUnitOfWork, UnitOfWorkSqlServer>();
services.AddSingleton<ITaskService, TaskService>();

AddAutoMapper(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddAutoMapper(IServiceCollection services)
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
