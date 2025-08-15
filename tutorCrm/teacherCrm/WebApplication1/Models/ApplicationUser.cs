using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string ParentFullName { get; set; }

    public string? ParentContact { get; set; }
}