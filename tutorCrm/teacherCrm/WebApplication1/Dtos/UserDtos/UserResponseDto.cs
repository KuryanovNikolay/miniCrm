namespace WebApplication1.Dtos.UserDtos;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string ParentFullName { get; set; }
    public string? ParentContact { get; set; }
    public string Role { get; set; }
}