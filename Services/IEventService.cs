using StudentEventManagementSystem.DTOs;
namespace StudentEventManagementSystem.Services;
public interface IEventService { Task<IEnumerable<EventResponseDto>> GetAllUpcomingEventsAsync(); Task<EventResponseDto?> GetEventByIdAsync(int id); Task<EventResponseDto> CreateEventAsync(CreateEventDto dto); Task<EventResponseDto?> UpdateEventAsync(int id, UpdateEventDto dto); Task<bool> DeleteEventAsync(int id); Task<IEnumerable<EventResponseDto>> SearchEventsAsync(string query); Task<IEnumerable<EventResponseDto>> FilterEventsAsync(string sort); }
