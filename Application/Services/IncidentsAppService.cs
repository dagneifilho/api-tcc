using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class IncidentsAppService : IIncidentsAppService
{

    private readonly IIncidentsRepository _repository;
    private readonly IAuthRepository _authRepository;
    private readonly ISectionsRepository _sectionsRepository;

    public IncidentsAppService(IIncidentsRepository repository,
                                IAuthRepository authRepository,
                                ISectionsRepository sectionsRepository)
    {
        _repository = repository;
        _authRepository = authRepository;
        _sectionsRepository = sectionsRepository;
    }
    public async Task<IncidentDTO> ReportIncident(CreateIncident incident)
    {
        var incidentDb = new Incident()
        {
            Description = incident.Description,
            Issuer = incident.IssuerId,
            Opened = DateTime.Now,
            SectionId = incident.SectionId
        };
        incidentDb.Id = await _repository.OpenIncident(incidentDb);
        
        var newIncident = await MapIncidentDto(incidentDb);

        return newIncident;
    }

    public async Task<IncidentDTO> CloseIncident(int incidentId, CloseIncident closeIncident)
    {
        var incidentDb = await _repository.GetIncidentById(incidentId);
        incidentDb.Closed = DateTime.Now;
        incidentDb.Solved = true;
        incidentDb.ClosedBy = closeIncident.UserId;
        await _repository.CloseIncident(incidentDb);
        var newIncident = await MapIncidentDto(incidentDb);
        return newIncident;
    }

    public async Task<IncidentDTO> GetIncidentById(int id)
    {
        var incident = await _repository.GetIncidentById(id);
        return await MapIncidentDto(incident);
    }

    public async Task<IList<IncidentDTO>> GetIncidents(IncidentsFilters filters)
    {
        var filtersDb = MapFilters(filters);
        var incidents = await _repository.GetIncidentsDb(filtersDb);
        var incidentsDto = new List<IncidentDTO>();
        
        foreach (var item in incidents)
        {
            incidentsDto.Add(await MapIncidentDto(item));
        }

        return incidentsDto;
    }

    private IncidentsFiltersDb MapFilters(IncidentsFilters filters)
    {
        return new IncidentsFiltersDb()
        {
            IssuerId = filters.IssuerId,
            ClosedById = filters.ClosedById,
            SectionId = filters.SectionId,
            Solved = filters.Solved
        };
    }
    private async Task<IncidentDTO> MapIncidentDto(Incident incident)
    {
        var issuerUser = await _authRepository.GetUserById(incident.Issuer);
        UserDb closedByUser = null;
        if (incident.ClosedBy is not null)
        {
            closedByUser = await _authRepository.GetUserById(incident.ClosedBy.Value);
        }

        var section = await _sectionsRepository.GetSectionById(incident.SectionId);

        return new IncidentDTO()
        {
            Closed = incident.Closed,
            ClosedById = incident.ClosedBy,
            ClosedByUsername = closedByUser != null ? closedByUser.Username : null,
            Description = incident.Description,
            Id = incident.Id,
            IssuerId = incident.Issuer,
            IssuerUsername = issuerUser.Username,
            Opened = incident.Opened,
            Solved = incident.Solved,
            SectionId = incident.SectionId,
            SectionName = section.Name
        };
    }
    public void Dispose()
    {
        _authRepository.Dispose();
        _repository.Dispose();
        GC.SuppressFinalize(this);
    }
}