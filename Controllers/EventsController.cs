using Microsoft.AspNetCore.Mvc;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Services;

namespace StudentEventManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventService eventService, ILogger<EventsController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllUpcomingEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var ev = await _eventService.GetEventByIdAsync(id);
        if (ev == null) return NotFound(new { message = $"Event {id} not found." });
        return Ok(ev);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _eventService.CreateEventAsync(dto);
        return CreatedAtAction(nameof(GetEventById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto dto)
    {
        var updated = await _eventService.UpdateEventAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var deleted = await _eventService.DeleteEventAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchEvents([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return BadRequest();
        return Ok(await _eventService.SearchEventsAsync(query));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterEvents([FromQuery] string sort = "date")
        => Ok(await _eventService.FilterEventsAsync(sort));
}
