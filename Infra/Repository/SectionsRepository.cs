using System.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Infra.Repository;

public class SectionsRepository : ISectionsRepository
{
    private readonly IDbConnection _connection;

    public SectionsRepository(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
    }
    


    public async Task<int> CreateSection(SectionDb section)
    {
        var parameters = new DynamicParameters();
        var query = @"INSERT INTO sections (Name, Description)
                        VALUES (@name, @description);
                        SELECT LAST_INSERT_ID();";
        parameters.Add("@name", section.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("@description", section.Description, DbType.String, ParameterDirection.Input);
        return await _connection.QueryFirstOrDefaultAsync<int>(query, parameters);
    }

    public async Task<SectionDb> GetSectionById(int id)
    {
        var parameters = new DynamicParameters();
        var query = @"SELECT Id, Name, Description FROM sections WHERE Id = @id";
        parameters.Add("@id", id, DbType.Int32,ParameterDirection.Input);
        return await _connection.QueryFirstOrDefaultAsync<SectionDb>(query, parameters);
    }

    public async Task<IList<SectionDb>> GetAllSections()
    {
        var query = @"SELECT Id, Name, Description FROM sections";
        return (await _connection.QueryAsync<SectionDb>(query)).ToList();
    }
    
    
    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}