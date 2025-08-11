using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

public class Lesson
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid SubjectId { get; set; }

    [Required]
    public DateTime LessonDate { get; set; }

    [Required]
    [Range(1, 480)]
    public int DurationMinutes { get; set; }

    [StringLength(200)]
    public string Topic { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Scheduled";

    [Range(0, 999999.99)]
    public decimal Price { get; set; }

    public string Notes { get; set; }

    public virtual ApplicationUser Teacher { get; set; }
    public virtual ApplicationUser Student { get; set; }
    public virtual Subject Subject { get; set; }
    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
}

