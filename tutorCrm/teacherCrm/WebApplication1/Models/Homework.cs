using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

public class Homework
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid? LessonId { get; set; }

    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? Deadline { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Assigned";

    public virtual Lesson Lesson { get; set; }
    public virtual ApplicationUser Teacher { get; set; }
    public virtual ApplicationUser Student { get; set; }
}