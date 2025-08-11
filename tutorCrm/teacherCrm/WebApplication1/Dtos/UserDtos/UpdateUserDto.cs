using System.ComponentModel.DataAnnotations;

public class UpdateUserDto
{
    [StringLength(100)]
    public string? FullName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? ParentFullName { get; set; }

    [StringLength(100)]
    public string? ParentContact { get; set; }
}