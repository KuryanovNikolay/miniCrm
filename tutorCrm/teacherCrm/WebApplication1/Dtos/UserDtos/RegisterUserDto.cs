using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.UserDtos;

/// <summary>
/// DTO для регистрации пользователя.
/// </summary>
public class RegisterUserDto
{
    /// <summary>
    /// Логин нового пользователя.
    /// </summary>
    [Required]
    public string Username { get; set; } = default!;

    /// <summary>
    /// Email нового пользователя.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Полное имя нового пользователя.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = default!;

    /// <summary>
    /// Телефон нового пользователя (опционально).
    /// </summary>
    [Phone]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Полное имя родителя (опционально).
    /// </summary>
    [StringLength(100)]
    public string? ParentFullName { get; set; }

    /// <summary>
    /// Контакт родителя (опционально).
    /// </summary>
    [StringLength(100)]
    public string? ParentContact { get; set; }

    /// <summary>
    /// Пароль нового пользователя.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}
