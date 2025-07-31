using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

public class Homework
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid LessonId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? Deadline { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Assigned";

    // Навигационное свойство
    public virtual Lesson Lesson { get; set; }
}

