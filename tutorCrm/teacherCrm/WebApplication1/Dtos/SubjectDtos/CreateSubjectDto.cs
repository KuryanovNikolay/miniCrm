using System.ComponentModel.DataAnnotations;

public class CreateSubjectDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public Guid TeacherId { get; set; }
}
