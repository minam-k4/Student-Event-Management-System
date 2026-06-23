using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;
namespace StudentEventManagementSystem.Services;
public class EventService : IEventService
{
    private readonly AppDbContext _ctx;
    public EventService(AppDbContext c) => _ctx = c;
    public async Task<IEnumerable<EventResponseDto>> GetAllUpcomingEventsAsync() => await _ctx.Events.Where(e => e.EventDate >= DateTime.UtcNow).Include(e => e.Registrations).Include(e => e.Feedbacks).Select(e => Map(e)).ToListAsync();
    public async Task<EventResponseDto?> GetEventByIdAsync(int id) { var e = await _ctx.Events.Include(x => x.Registrations).Include(x => x.Feedbacks).FirstOrDefaultAsync(x => x.Id == id); return e == null ? null : Map(e); }
    public async Task<EventResponseDto> CreateEventAsync(CreateEventDto d) { var e = new Event { Name=d.Name, Description=d.Description, Venue=d.Venue, EventDate=d.EventDate, CreatedAt=DateTime.UtcNow }; _ctx.Events.Add(e); await _ctx.SaveChangesAsync(); return Map(e); }
    public async Task<EventResponseDto?> UpdateEventAsync(int id, UpdateEventDto d) { var e = await _ctx.Events.FindAsync(id); if(e==null)return null; if(d.Name!=null)e.Name=d.Name; if(d.Description!=null)e.Description=d.Description; if(d.Venue!=null)e.Venue=d.Venue; if(d.EventDate.HasValue)e.EventDate=d.EventDate.Value; await _ctx.SaveChangesAsync(); return Map(e); }
    public async Task<bool> DeleteEventAsync(int id) { var e = await _ctx.Events.FindAsync(id); if(e==null)return false; _ctx.Events.Remove(e); await _ctx.SaveChangesAsync(); return true; }
    public async Task<IEnumerable<EventResponseDto>> SearchEventsAsync(string q) => await _ctx.Events.Include(e => e.Registrations).Include(e => e.Feedbacks).Where(e => e.Name.ToLower().Contains(q.ToLower())||e.Venue.ToLower().Contains(q.ToLower())).Select(e => Map(e)).ToListAsync();
    public async Task<IEnumerable<EventResponseDto>> FilterEventsAsync(string s) => await _ctx.Events.Include(e => e.Registrations).Include(e => e.Feedbacks).OrderBy(s=="venue"?e => e.Venue:e => e.EventDate.ToString()).Select(e => Map(e)).ToListAsync();
    private static EventResponseDto Map(Event e) => new(){ Id=e.Id,Name=e.Name,Description=e.Description,Venue=e.Venue,EventDate=e.EventDate,CreatedAt=e.CreatedAt,RegistrationsCount=e.Registrations?.Count??0,AverageRating=e.Feedbacks!=null&&e.Feedbacks.Any()?e.Feedbacks.Average(f=>f.Rating):0 };
}
