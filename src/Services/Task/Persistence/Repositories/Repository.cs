using System.Data.SqlClient;

namespace Persistence.Repositories;

public abstract class Repository
{
    protected SqlConnection? Context;
    protected SqlTransaction? Transaction;

    protected SqlCommand CreateCommand(string query)
    {
        return new SqlCommand(query, Context, Transaction);
    }
}