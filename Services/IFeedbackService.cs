using StudentEventManagementSystem.DTOs;
namespace StudentEventManagementSystem.Services;
public interface IFeedbackService { Task<FeedbackResponseDto> SubmitFeedbackAsync(CreateFeedbackDto dto); Task<IEnumerable<FeedbackResponseDto>> GetFeedbackByEventAsync(int eventId); }
