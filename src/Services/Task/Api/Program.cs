using Api.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddAutoMapper(builder.Services);

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
