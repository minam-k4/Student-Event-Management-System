using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;
namespace StudentEventManagementSystem.Services;
public class FeedbackService : IFeedbackService
{
    private readonly AppDbContext _ctx;
    public FeedbackService(AppDbContext c) => _ctx = c;
    public async Task<FeedbackResponseDto> SubmitFeedbackAsync(CreateFeedbackDto d) {
        var st = await _ctx.Students.FindAsync(d.StudentId) ?? throw new KeyNotFoundException($"Student {d.StudentId} not found.");
        var ev = await _ctx.Events.FindAsync(d.EventId) ?? throw new KeyNotFoundException($"Event {d.EventId} not found.");
        if(ev.EventDate > DateTime.UtcNow) throw new InvalidOperationException("Feedback only allowed after the event date.");
        if(!await _ctx.Registrations.AnyAsync(r => r.StudentId==d.StudentId&&r.EventId==d.EventId)) throw new InvalidOperationException("Only registered participants can submit feedback.");
        var f = new Feedback { StudentId=d.StudentId,EventId=d.EventId,Rating=d.Rating,Comment=d.Comment,SubmittedAt=DateTime.UtcNow };
        _ctx.Feedbacks.Add(f); await _ctx.SaveChangesAsync();
        return new FeedbackResponseDto { Id=f.Id,StudentId=st.Id,StudentName=st.Name,EventId=ev.Id,EventName=ev.Name,Rating=f.Rating,Comment=f.Comment,SubmittedAt=f.SubmittedAt }; }
    public async Task<IEnumerable<FeedbackResponseDto>> GetFeedbackByEventAsync(int eid) => await _ctx.Feedbacks.Where(f => f.EventId==eid).Include(f => f.Student).Include(f => f.Event).Select(f => new FeedbackResponseDto { Id=f.Id,StudentId=f.StudentId,StudentName=f.Student.Name,EventId=f.EventId,EventName=f.Event.Name,Rating=f.Rating,Comment=f.Comment,SubmittedAt=f.SubmittedAt }).ToListAsync();
}
