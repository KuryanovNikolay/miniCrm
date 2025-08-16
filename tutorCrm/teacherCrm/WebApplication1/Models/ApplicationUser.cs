using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Класс пользователя приложения, расширяющий IdentityUser.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    [Required]
    public string FullName { get; set; }

    /// <summary>
    /// Дата регистрации пользователя.
    /// </summary>
    [Required]
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Полное имя родителя пользователя.
    /// </summary>
    [StringLength(100)]
    public string ParentFullName { get; set; }

    /// <summary>
    /// Контакт родителя пользователя (опционально).
    /// </summary>
    [StringLength(100)]
    public string? ParentContact { get; set; }
}
