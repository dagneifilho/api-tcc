using System.Collections.Specialized;
using System.Net.Http.Headers;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("sections")]
[Authorize("Manager")]
public class SectionsController : ControllerBase
{
    private readonly ISectionsAppService _appService;

    public SectionsController(ISectionsAppService appService)
    {
        _appService = appService;
    }
    [HttpPost]
    public async Task<IActionResult> NewSection([FromBody] CreateSection section)
    {
        if (!ModelState.IsValid)
            return StatusCode(400);
        var sectionDto = await _appService.CreateSection(section);

        return StatusCode(201, sectionDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSectionById([FromRoute] int id)
    {
        var section = await _appService.GetSectionById(id);
        if (section is null)
            return StatusCode(404);
        return StatusCode(200, section);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSections()
    {
        var sections = await _appService.GetAllSections();
        if (sections.Count == 0)
            return StatusCode(204);
        return StatusCode(200, sections);
    }
}