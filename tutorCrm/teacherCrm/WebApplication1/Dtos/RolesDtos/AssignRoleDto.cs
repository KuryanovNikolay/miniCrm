using System;

namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// DTO для назначения одной роли пользователю.
/// </summary>
public class AssignRoleDto
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
