using Application.Interfaces.Persistence;
using Microsoft.Extensions.Configuration;

namespace Persistence.UnitOfWork;

public class UnitOfWorkSqlServer(IConfiguration configuration) : IUnitOfWork
{
    private readonly IConfiguration _configuration = configuration;

    public IUnitOfWorkAdapter Create()
    {
        var connectionString = _configuration.GetConnectionString("SqlConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("The connection string is null or empty.");
        }

        return new UnitOfWorkSqlServerAdapter(connectionString);
    }
}