using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.UserDtos;

/// <summary>
/// DTO для входа пользователя.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Флаг "Запомнить меня".
    /// </summary>
    public bool RememberMe { get; set; }
}
