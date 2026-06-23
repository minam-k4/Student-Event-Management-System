using System.ComponentModel.DataAnnotations;
namespace StudentEventManagementSystem.DTOs;
public class CreateRegistrationDto { [Required] public int StudentId { get; set; } [Required] public int EventId { get; set; } }
public class RegistrationResponseDto { public int Id { get; set; } public int StudentId { get; set; } public string StudentName { get; set; } = string.Empty; public int EventId { get; set; } public string EventName { get; set; } = string.Empty; public DateTime RegisteredAt { get; set; } }
