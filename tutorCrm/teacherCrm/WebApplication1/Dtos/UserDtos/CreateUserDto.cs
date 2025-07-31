using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.UserDtos;

public class CreateUserDto
{
    public CreateUserDto(string username, string password, string email, string fullName, string phoneNumber, string parentFullName, string parentContact)
    {
        Username = username;
        Password = password;
        Email = email;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        ParentFullName = parentFullName;
        ParentContact = parentContact;
    }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    [Required]
    public string ParentFullName { get; set; }

    public string ParentContact { get; set; }
}

