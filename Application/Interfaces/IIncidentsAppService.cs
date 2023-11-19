using System.Runtime.CompilerServices;
using Application.Models;

namespace Application.Interfaces;

public interface IIncidentsAppService : IDisposable
{
    Task<IncidentDTO> ReportIncident(CreateIncident incident);
    Task<IncidentDTO> CloseIncident(int incidentId, CloseIncident closeIncident);
    Task<IncidentDTO> GetIncidentById(int id);
    Task<IList<IncidentDTO>> GetIncidents(IncidentsFilters filters);
    
}