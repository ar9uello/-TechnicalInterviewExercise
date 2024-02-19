using Application.Interfaces.Persistence;
using System.Data.SqlClient;

namespace Persistence.UnitOfWork;

public class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
{
    private SqlConnection? Connection { get; set; }
    private SqlTransaction Transaction { get; set; }
    public IUnitOfWorkRepository Repositories { get; set; }

    public UnitOfWorkSqlServerAdapter(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
        Connection.Open();

        Transaction = Connection.BeginTransaction();

        Repositories = new UnitOfWorkSqlServerRepository(Connection, Transaction);
    }

    public void Dispose()
    {
        Transaction?.Dispose();

        if (Connection != null)
        {
            Connection.Close();
            Connection.Dispose();
            Connection = null;
        }

        GC.SuppressFinalize(this);
    }

    public void SaveChanges()
    {
        Transaction.Commit();
    }
}