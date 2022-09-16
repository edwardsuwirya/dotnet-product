using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Product_CRUD;

public class DapperContext
{
    private readonly string _connString;
    public DapperContext(string connString) => _connString = connString;

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connString);
}