using Microsoft.AspNetCore.Mvc;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Services;

namespace StudentEventManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService s) => _feedbackService = s;

    [HttpPost]
    public async Task<IActionResult> SubmitFeedback([FromBody] CreateFeedbackDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try { return CreatedAtAction(nameof(GetFeedbackByEvent), new { eventId = dto.EventId }, await _feedbackService.SubmitFeedbackAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return UnprocessableEntity(new { message = ex.Message }); }
    }

    [HttpGet("event/{eventId:int}")]
    public async Task<IActionResult> GetFeedbackByEvent(int eventId)
        => Ok(await _feedbackService.GetFeedbackByEventAsync(eventId));
}
