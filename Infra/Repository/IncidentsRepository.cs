using System.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Infra.Repository;

public class IncidentsRepository : IIncidentsRepository
{
    private readonly IDbConnection _connection;

    public IncidentsRepository(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
    }

    public async Task<int> OpenIncident(Incident incident)
    {
        DynamicParameters parameters = new DynamicParameters();
        var query = @"INSERT INTO incidents 
                (
                    Description,
                    Issuer,
                    Solved,
                    Opened,
                    SectionId
                ) 
                 VALUES (
                         @description,
                         @issuer,
                         @solved,
                         @opened,
                         @sectionId
                 );
            SELECT LAST_INSERT_ID()";
        parameters.Add("@description",incident.Description, DbType.String, ParameterDirection.Input);
        parameters.Add("@issuer",incident.Issuer, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@solved",incident.Solved, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("@opened",incident.Opened, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("@sectionId",incident.SectionId, DbType.Int32, ParameterDirection.Input);


        return await _connection.QueryFirstOrDefaultAsync<int>(query, parameters);
    }

    public async Task<Incident> GetIncidentById(int id)
    {
        var parameters = new DynamicParameters();
        var query = @"SELECT Id,
            Description,
            Issuer,
            Solved,
            ClosedBy,
            Opened,
            Closed,
            SectionId 
            FROM incidents 
            WHERE Id = @id ;";
        parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
        return await _connection.QueryFirstOrDefaultAsync<Incident>(query, parameters);
    }

    public async Task CloseIncident(Incident incident)
    {
        var parameters = new DynamicParameters();
        var query = @"UPDATE incidents
                        SET ClosedBy = @closedBy,
                            Closed = @closed,
                            Solved = @solved
                        WHERE Id = @id ";
        parameters.Add("@closedBy", incident.ClosedBy, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@closed", incident.Closed, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("@solved", incident.Solved, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("@id", incident.Id, DbType.Int32, ParameterDirection.Input);

        await _connection.ExecuteAsync(query, parameters);
    }

    public async Task<IList<Incident>> GetIncidentsDb(IncidentsFiltersDb filters)
    {
        var parameters = new DynamicParameters();
        var query = @"SELECT Id,
            Description,
            Issuer,
            Solved,
            ClosedBy,
            Opened,
            Closed,
            SectionId 
            FROM incidents 
            WHERE 1=1 ";
        if (filters.IssuerId is not null)
        {
            query += @" AND IssuerId = @issuerId ";
            parameters.Add("@issuerId", filters.IssuerId, DbType.Int32, ParameterDirection.Input);
        }

        if (filters.ClosedById is not null)
        {
            query += @" AND ClosedBy = @closedById ";
            parameters.Add("@closedById", filters.ClosedById, DbType.Int32, ParameterDirection.Input);
        }

        if (filters.Solved is not null)
        {
            query += @" AND Solved = @solved ";
            parameters.Add("@solved", filters.Solved, DbType.Boolean, ParameterDirection.Input);
        }

        if (filters.SectionId is not null)
        {
            query += @" AND SectionId = @sectionId ";
            parameters.Add("@sectionId", filters.SectionId, DbType.Int32, ParameterDirection.Input);
        }

        return (await _connection.QueryAsync<Incident>(query, parameters)).ToList();
    }


    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

}