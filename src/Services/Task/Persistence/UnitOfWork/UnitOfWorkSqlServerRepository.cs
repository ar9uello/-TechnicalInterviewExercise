using Application.Interfaces.Persistence;
using Persistence.Repositories;
using System.Data.SqlClient;

namespace Persistence.UnitOfWork;

public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
{
    public ITaskRepository TaskRepository { get; }

    public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
    {
        TaskRepository = new TaskRepository(context, transaction);
    }

}