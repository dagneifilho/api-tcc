using Domain.Models;

namespace Domain.Interfaces;

public interface ISectionsRepository :IDisposable
{
    Task<SectionDb> GetSectionById(int id);
    Task<IList<SectionDb>> GetAllSections();
    Task<int> CreateSection(SectionDb section);
}