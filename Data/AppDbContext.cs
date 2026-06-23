using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Models;

namespace StudentEventManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Registration>().HasIndex(r => new { r.StudentId, r.EventId }).IsUnique();

        modelBuilder.Entity<Registration>().HasOne(r => r.Student).WithMany(s => s.Registrations).HasForeignKey(r => r.StudentId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Registration>().HasOne(r => r.Event).WithMany(e => e.Registrations).HasForeignKey(r => r.EventId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Feedback>().HasOne(f => f.Student).WithMany(s => s.Feedbacks).HasForeignKey(f => f.StudentId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Feedback>().HasOne(f => f.Event).WithMany(e => e.Feedbacks).HasForeignKey(b => b.EventId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Name = "Annual Tech Summit 2025", Description = "Annual technology conference", Venue = "Main Auditorium", EventDate = new DateTime(2025,10,15,URcKind.Utc), CreatedAt = new DateTime(2025,1,2,UtcKind.Utc) },
            new Event { Id = 2, Name = "Web Engineering Workshop", Description = "Hands-on ASP.NET Core workshop", Venue = "Lab Block B", EventDate = new DateTime(2026,8,1,UtcKind.Utc), CreatedAt = new DateTime(2026,1,1,UtcKind.Utc) },
            new Event { Id = 3, Name = "Hackathon Spring 2026", Description = "24-hour coding competition", Venue = "Innovation Hub", EventDate = new DateTime(2026,9,20,UtcKind.Utc), CreatedAt = new DateTime(2026,1,1,UtcKind.Utc) }
        );
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Minam Khan", Email = "minam@iqra.edu.pk", RollNumber = "61735", CreatedAt = new DateTime(2026,1,1,UtcKind.Utc) },
            new Student { Id = 2, Name = "Ali Raza", Email = "ali.raza@iqra.edu.pk", RollNumber = "61800", CreatedAt = new DateTime(2026,1,1,UtcKind.Utc) }
        );
    }
}
