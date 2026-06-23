using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;
namespace StudentEventManagementSystem.Services;
public class RegistrationService : IRegistrationService
{
    private readonly AppDbContext _ctx;
    public RegistrationService(AppDbContext c) => _ctx = c;
    public async Task<RegistrationResponseDto> RegisterStudentAsync(CreateRegistrationDto d) {
        var st = await _ctx.Students.FindAsync(d.StudentId) ?? throw new KeyNotFoundException($"Student {d.StudentId} not found.");
        var ev = await _ctx.Events.FindAsync(d.EventId) ?? throw new KeyNotFoundException($"Event {d.EventId} not found.");
        if(await _ctx.Registrations.AnyAsync(r => r.StudentId==d.StudentId&&r.EventId==d.EventId)) throw new InvalidOperationException("Student already registered for this event.");
        var r = new Registration { StudentId=d.StudentId,EventId=d.EventId,RegisteredAt=DateTime.UtcNow };
        _ctx.Registrations.Add(r); await _ctx.SaveChangesAsync();
        return new RegistrationResponseDto { Id=r.Id,StudentId=st.Id,StudentName=st.Name,EventId=ev.Id,EventName=ev.Name,RegisteredAt=r.RegisteredAt }; }
    public async Task<IEnumerable<RegistrationResponseDto>> GetRegistrationsByEventAsync(int eid) => await _ctx.Registrations.Where(r => r.EventId==eid).Include(r => r.Student).Include(r => r.Event).Select(r => new RegistrationResponseDto { Id=r.Id,StudentId=r.StudentId,StudentName=r.Student.Name,EventId=r.EventId,EventName=r.Event.Name,RegisteredAt=r.RegisteredAt }).ToListAsync();
}
