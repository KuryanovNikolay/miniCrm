namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// DTO для отображения информации о роли.
/// </summary>
public class RoleDto
{
    /// <summary>
    /// Идентификатор роли.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название роли.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание роли.
    /// </summary>
    public string Description { get; set; }
}
