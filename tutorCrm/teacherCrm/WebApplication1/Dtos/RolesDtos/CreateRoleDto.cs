using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// DTO для создания новой роли.
/// </summary>
public class CreateRoleDto
{
    /// <summary>
    /// Название роли.
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "Название роли не должно превышать 50 символов")]
    public string Name { get; set; }

    /// <summary>
    /// Описание роли.
    /// </summary>
    [StringLength(255, ErrorMessage = "Описание роли не должно превышать 255 символов")]
    public string Description { get; set; }
}
