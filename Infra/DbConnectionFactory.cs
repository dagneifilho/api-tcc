using System.Data;
using Domain.Interfaces;
using MySqlConnector;

namespace Infra;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly DbConfig _dbConfig;

    public DbConnectionFactory(DbConfig dbConfig)
    {
        _dbConfig = dbConfig;
    }
    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_dbConfig.ConnectionString);
    }
}