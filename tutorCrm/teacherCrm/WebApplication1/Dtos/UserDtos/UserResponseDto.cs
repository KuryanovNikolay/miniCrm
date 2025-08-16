using System;

namespace WebApplication1.Dtos.UserDtos;

/// <summary>
/// DTO для ответа с информацией о пользователе.
/// </summary>
public class UserResponseDto
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Email пользователя.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Телефон пользователя.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Дата регистрации пользователя.
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Полное имя родителя пользователя.
    /// </summary>
    public string ParentFullName { get; set; }

    /// <summary>
    /// Контакт родителя пользователя.
    /// </summary>
    public string? ParentContact { get; set; }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public string Role { get; set; }
}
