using Application.Models;

namespace Application.Interfaces;

public interface ISectionsAppService : IDisposable
{
    Task<SectionDTO> CreateSection(CreateSection section);
    Task<SectionDTO> GetSectionById(int id);
    Task<IList<SectionDTO>> GetAllSections();
}