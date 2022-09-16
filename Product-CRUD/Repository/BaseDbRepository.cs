using System.Data;
using Product_CRUD;

namespace ProductCRUD.Repository;

public abstract class BaseDbRepository
{
    private IDbConnection? _con = null;

    protected void WithConn(Action<IDbConnection> actionSql)
    {
        try
        {
            if (_con is null)
            {
                throw new Exception("Connection is null");
            }

            actionSql(_con);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected void WithTrx(Action<IDbConnection, IDbTransaction> actionSql)
    {
        try
        {
            if (_con is null)
            {
                throw new Exception("Connection is null");
            }

            _con.Open();
            using var transaction = _con.BeginTransaction();
            try
            {
                actionSql(_con, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                throw;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected void GetConnection(DapperContext context)
    {
        _con = context.CreateConnection();
    }
}