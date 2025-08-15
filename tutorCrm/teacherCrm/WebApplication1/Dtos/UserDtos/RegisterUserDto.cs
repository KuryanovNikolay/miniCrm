public class RegisterUserDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? ParentFullName { get; set; }
    public string? ParentContact { get; set; }
    public string Password { get; set; } = default!;
}
