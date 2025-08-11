using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    public Guid? LessonId { get; set; }

    public virtual ApplicationUser Teacher { get; set; }
    public virtual ApplicationUser Student { get; set; }
    public virtual Lesson Lesson { get; set; }
}

