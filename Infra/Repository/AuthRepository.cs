using System.Data;
using System.Runtime.CompilerServices;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Infra.Repository;

public class AuthRepository : IAuthRepository
{
    private IDbConnection _connection;

    public AuthRepository(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
    }

    public async Task<UserDb> GetUserByUsername(string username)
    {
        DynamicParameters parameters = new DynamicParameters();
        var query = @"
            SELECT Id,
            Username,
            PasswordHash,
            PasswordSalt,
            Role
            FROM users 
            WHERE Username = @username
        ";
        
        parameters.Add("@username",username,DbType.String,ParameterDirection.Input);
        return await _connection.QueryFirstOrDefaultAsync<UserDb>(query, parameters);
    }

    public async Task CreateUser(UserDb user)
    {
        var parameters = new DynamicParameters();
        var query = @"INSERT INTO users (Username, PasswordHash, PasswordSalt, Role)
                        VALUES (@username, @passwordHash, @passwordSalt, @role)";
        parameters.Add("@username",user.Username,DbType.String,ParameterDirection.Input);
        parameters.Add("@passwordHash", user.PasswordHash, DbType.Binary, ParameterDirection.Input);
        parameters.Add("@passwordSalt",user.PasswordSalt, DbType.Binary, ParameterDirection.Input);
        parameters.Add("@role", user.Role, DbType.String, ParameterDirection.Input);

        await _connection.ExecuteAsync(query, parameters);
    }

    public async Task<UserDb> GetUserById(int id)
    {
        DynamicParameters parameters = new DynamicParameters();
        var query = @"
            SELECT Id,
            Username,
            PasswordHash,
            PasswordSalt,
            Role
            FROM users 
            WHERE Id = @id
        ";
        
        parameters.Add("@id",id,DbType.Int32,ParameterDirection.Input);
        return await _connection.QueryFirstOrDefaultAsync<UserDb>(query, parameters);
    }

    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}