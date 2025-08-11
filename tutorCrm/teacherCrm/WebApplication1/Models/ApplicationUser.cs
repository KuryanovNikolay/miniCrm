using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser<Guid>
{
    // Полное имя пользователя (обязательно)
    public string FullName { get; set; }

    // Дата регистрации (заполняется автоматически)
    public DateTime RegistrationDate { get; set; }

    // Имя родителя (обязательно)
    public string ParentFullName { get; set; }

    // Контакт родителя (необязательно)
    public string? ParentContact { get; set; }
}