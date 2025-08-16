using System;

namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// DTO для удаления роли у пользователя.
/// </summary>
public class RemoveRoleDto
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор роли.
    /// </summary>
    public Guid RoleId { get; set; }
}
