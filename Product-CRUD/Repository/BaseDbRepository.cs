using Npgsql;

namespace ProductCRUD.Repository;

public abstract class BaseDbRepository
{
    private NpgsqlConnection? _con = null;

    private Task WithConn(Func<NpgsqlConnection, Task> actionSql)
    {
        if (_con is not null)
        {
            return actionSql(_con);
        }
        else
        {
            throw new Exception("Connection is null");
        }
    }

    protected void GetConnection(string connectionString)
    {
        _con = new NpgsqlConnection(connectionString);
    }

    private void GenerateParameters(NpgsqlCommand cmd, List<string>? parameters)
    {
        if (parameters == null || parameters.Count == 0) return;
        foreach (var p in parameters)
        {
            cmd.Parameters.AddWithValue(p);
        }
    }

    protected Task QuerySql(string sql, List<string>? parameters, Action<NpgsqlDataReader> reader)
    {
        return WithConn((con) => Task.Run(async () =>
        {
            try
            {
                await con.OpenAsync();
                await using var cmd = new NpgsqlCommand(sql, con);
                GenerateParameters(cmd, parameters);
                await using var rdr = await cmd.ExecuteReaderAsync();
                reader(rdr);
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await con.CloseAsync();
            }
        }));
    }

    protected Task ExecSql(string sql, List<string> parameters)
    {
        return WithConn((con) => Task.Run(async () =>
        {
            try
            {
                await con.OpenAsync();
                await using var cmd = new NpgsqlCommand(sql, con);
                GenerateParameters(cmd, parameters);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await con.CloseAsync();
            }
        }));
    }

    protected Task ExecBatchInsertSql(List<NpgsqlBatchCommand> commands)
    {
        return WithConn((con) => Task.Run(async () =>
        {
            await con.OpenAsync();
            try
            {
                await using var batch = new NpgsqlBatch(con);
                foreach (var bc in commands)
                {
                    batch.BatchCommands.Add(bc);
                }

                await batch.ExecuteNonQueryAsync();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await con.CloseAsync();
            }
        }));
    }
}