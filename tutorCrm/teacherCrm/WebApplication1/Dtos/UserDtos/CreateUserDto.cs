using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.UserDtos;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string ParentFullName { get; set; }
    public string? ParentContact { get; set; }
    public string Role { get; set; }
}