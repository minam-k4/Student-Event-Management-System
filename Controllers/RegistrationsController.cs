using Microsoft.AspNetCore.Mvc;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Services;

namespace StudentEventManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _svc;

    public RegistrationsController(IRegistrationService s) => _svc = s;

    [HttpPost]
    public async Task<IActionResult> RegisterStudent([FromBody] CreateRegistrationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return CreatedAtAction(nameof(GetRegistrationsByEvent), new { eventId = dto.EventId }, await _svc.RegisterStudentAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    [HttpGet("event/{eventId:int}")]
    public async Task<IActionResult> GetRegistrationsByEvent(int eventId)
        => Ok(await _svc.GetRegistrationsByEventAsync(eventId));
}
