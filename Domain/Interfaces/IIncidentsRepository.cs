using Domain.Models;

namespace Domain.Interfaces;

public interface IIncidentsRepository : IDisposable
{
    Task<int> OpenIncident(Incident incident);
    Task<Incident> GetIncidentById(int id);
    Task CloseIncident(Incident incident);
    Task<IList<Incident>> GetIncidentsDb(IncidentsFiltersDb filters);
}