using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.UserDtos;

/// <summary>
/// DTO для создания нового пользователя.
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Email пользователя.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    /// <summary>
    /// Телефон пользователя.
    /// </summary>
    [Phone]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Полное имя родителя (если есть).
    /// </summary>
    [StringLength(100)]
    public string ParentFullName { get; set; }

    /// <summary>
    /// Контакт родителя (если есть).
    /// </summary>
    [StringLength(100)]
    public string? ParentContact { get; set; }

    /// <summary>
    /// Роль пользователя (например, Student, Teacher, Admin).
    /// </summary>
    [Required]
    public string Role { get; set; }
}
