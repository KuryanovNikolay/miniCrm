using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.UserDtos;

/// <summary>
/// DTO для обновления данных пользователя.
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    [StringLength(100)]
    public string? FullName { get; set; }

    /// <summary>
    /// Email пользователя.
    /// </summary>
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Телефон пользователя.
    /// </summary>
    [Phone]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Полное имя родителя.
    /// </summary>
    [StringLength(100)]
    public string? ParentFullName { get; set; }

    /// <summary>
    /// Контакт родителя.
    /// </summary>
    [StringLength(100)]
    public string? ParentContact { get; set; }
}
