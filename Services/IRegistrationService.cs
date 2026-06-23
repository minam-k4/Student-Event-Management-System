using StudentEventManagementSystem.DTOs;
namespace StudentEventManagementSystem.Services;
public interface IRegistrationService { Task<RegistrationResponseDto> RegisterStudentAsync(CreateRegistrationDto dto); Task<IEnumerable<RegistrationResponseDto>> GetRegistrationsByEventAsync(int eventId); }
