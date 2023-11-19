using System.Collections.Specialized;
using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class SectionsAppService : ISectionsAppService
{
    private readonly ISectionsRepository _reposiotry;

    public SectionsAppService(ISectionsRepository repository)
    {
        _reposiotry = repository;
    }


    public async Task<SectionDTO> CreateSection(CreateSection section)
    {

        var sectionDb = CreateSectionDb(section);
        var id = await _reposiotry.CreateSection(sectionDb);
        return new SectionDTO()
        {
            Id = id,
            Name = section.Name,
            Description = section.Description,

        };
    }

    public async Task<SectionDTO> GetSectionById(int id)
    {
        var sectionDb = await _reposiotry.GetSectionById(id);
        if (sectionDb is null)
            return null;
        var sectionDto = MapSection(sectionDb);
        return sectionDto;
    }

    public async Task<IList<SectionDTO>> GetAllSections()
    {
        var sections = await _reposiotry.GetAllSections();
        var listRtn = new List<SectionDTO>();
        
        foreach (var sectionDb in sections)
        {
            listRtn.Add(MapSection(sectionDb));
        }

        return listRtn;
    }
    private SectionDTO MapSection(SectionDb sectionDb)
    {
        return new SectionDTO()
        {
            Id = sectionDb.Id,
            Name = sectionDb.Name,
            Description = sectionDb.Description
        };
    }
    private SectionDb CreateSectionDb(CreateSection section)
    {
        return new SectionDb()
        {
            Description = section.Description,
            Name = section.Name
        };
    } 
    public void Dispose()
    {
        _reposiotry.Dispose();
        GC.SuppressFinalize(this);
    }

}