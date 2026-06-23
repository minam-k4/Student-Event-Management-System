using System.ComponentModel.DataAnnotations;
namespace StudentEventManagementSystem.Models;
public class Feedback { public int Id { get; set; } [Required] public int StudentId { get; set; } [Required] public int EventId { get; set; } [Required][Range(1,5)] public int Rating { get; set; } [MaxLength(1000)] public string Comment { get; set; } = string.Empty; public DateTime SubmittedAt { get; set; } = DateTime.UtcNow; public Student Student { get; set; } = null!; public Event Event { get; set; } = null!; }
