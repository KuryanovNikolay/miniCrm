using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}

