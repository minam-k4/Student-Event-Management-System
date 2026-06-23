using System.ComponentModel.DataAnnotations;
namespace StudentEventManagementSystem.Models;
public class Registration { public int Id { get; set; } [Required] public int StudentId { get; set; } [Required] public int EventId { get; set; } public DateTime RegisteredAt { get; set; } = DateTime.UtcNow; public Student Student { get; set; } = null!; public Event Event { get; set; } = null!; }
