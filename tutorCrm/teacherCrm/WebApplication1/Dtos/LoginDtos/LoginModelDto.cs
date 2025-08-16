using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.LoginDtos;

/// <summary>
/// DTO для передачи данных при логине пользователя.
/// </summary>
public class LoginModelDto
{
    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string Email { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "Пароль обязателен")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Флаг "Запомнить меня" для долгого входа.
    /// </summary>
    public bool RememberMe { get; set; }
}
