using System.Xml.Schema;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentsAppService _appService;

    public IncidentsController(IIncidentsAppService appService)
    {
        _appService = appService;
    }

    [HttpPost]
    public async Task<IActionResult> ReportIncident([FromBody] CreateIncident incident)
    {
        if (!ModelState.IsValid)
            return StatusCode(400);
        var result = await _appService.ReportIncident(incident);
        return StatusCode(201, result);
    }
    [Authorize("Manager")]
    [HttpPost("{incidentId}/close-incident")]
    public async Task<IActionResult> CloseIncident([FromRoute] int incidentId, [FromBody] CloseIncident closeIncident)
    {
        if (!ModelState.IsValid)
            return StatusCode(400);
        var result = await _appService.CloseIncident(incidentId, closeIncident);

        if (result is null)
            return StatusCode(404);
        return StatusCode(200, result);
    }

    [Authorize("Manager")]
    [HttpGet]
    public async Task<IActionResult> GetIncidents([FromQuery] IncidentsFilters filters)
    {
        if (!ModelState.IsValid)
            return StatusCode(400);
        var result = await _appService.GetIncidents(filters);
        if (result.Count == 0)
            return StatusCode(204);
        return StatusCode(200, result);
    }

    [Authorize("Manager")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIncidentById(int id)
    {
        var result = await _appService.GetIncidentById(id);
        if (result is null)
            return StatusCode(404);
        return StatusCode(200, result);
    }
}