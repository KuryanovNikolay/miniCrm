using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// DTO для назначения нескольких ролей пользователю.
/// </summary>
public class AssignMultipleRolesDto
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Список идентификаторов ролей для назначения.
    /// </summary>
    [Required]
    [MinLength(1, ErrorMessage = "Необходимо указать хотя бы одну роль")]
    public IEnumerable<Guid> RoleIds { get; set; }
}
