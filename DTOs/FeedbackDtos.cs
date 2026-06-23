using System.ComponentModel.DataAnnotations;
namespace StudentEventManagementSystem.DTOs;
public class CreateFeedbackDto { [Required] public int StudentId { get; set; } [Required] public int EventId { get; set; } [Required][Range(1,5)] public int Rating { get; set; } [MaxLength(1000)] public string Comment { get; set; } = string.Empty; }
public class FeedbackResponseDto { public int Id { get; set; } public int StudentId { get; set; } public string StudentName { get; set; } = string.Empty; public int EventId { get; set; } public string EventName { get; set; } = string.Empty; public int Rating { get; set; } public string Comment { get; set; } = string.Empty; public DateTime SubmittedAt { get; set; } }
