namespace WebApplication1.Dtos.RolesDtos;

/// <summary>
/// Результат назначения ролей пользователю.
/// </summary>
/// <param name="TotalRolesRequested">Общее количество запрошенных ролей</param>
/// <param name="SuccessfullyAssigned">Количество успешно назначенных ролей</param>
/// <param name="Message">Дополнительное сообщение</param>
public record RoleAssignmentResult(
    int TotalRolesRequested,
    int SuccessfullyAssigned,
    string Message = "")
{
    /// <summary>
    /// Проверка, назначены ли все роли.
    /// </summary>
    public bool AllAssigned => TotalRolesRequested == SuccessfullyAssigned;
}
