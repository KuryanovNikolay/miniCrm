using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// DTO для обновления информации о роли.
/// </summary>
public class UpdateRoleDto
{
    /// <summary>
    /// Новое название роли (необязательное поле).
    /// </summary>
    [StringLength(50, ErrorMessage = "Название роли не должно превышать 50 символов")]
    public string? Name { get; set; }

    /// <summary>
    /// Новое описание роли (необязательное поле).
    /// </summary>
    [StringLength(255, ErrorMessage = "Описание роли не должно превышать 255 символов")]
    public string? Description { get; set; }
}
