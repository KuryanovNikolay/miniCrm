using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

public class Subscription
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid SubjectId { get; set; }

    [Required]
    [Range(1, 100)]
    public int LessonsTotal { get; set; }

    [Required]
    [Range(0, 100)]
    public int LessonsLeft { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Required]
    public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;

    public DateTime? EndDate { get; set; }

    // Навигационные свойства
    public virtual User Teacher { get; set; }
    public virtual User Student { get; set; }
    public virtual Subject Subject { get; set; }
}

