namespace WebApplication1.Dtos.HomeworkDtos;

/// <summary>
/// DTO для отображения информации о задании.
/// Используется при возврате данных клиенту.
/// </summary>
public class HomeworkDto
{
    /// <summary>
    /// Уникальный идентификатор задания.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Идентификатор урока, к которому относится задание.
    /// </summary>
    public Guid LessonId { get; set; }
    /// <summary>
    /// Идентификатор преподавателя, назначившего задание.
    /// </summary>
    public Guid TeacherId { get; set; }
    /// <summary>
    /// Идентификатор студента, которому назначено задание.
    /// </summary>
    public Guid StudentId { get; set; }
    /// <summary>
    /// Заголовок задания.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Подробное описание задания.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Дата и время дедлайна.
    /// </summary>
    public DateTime? Deadline { get; set; }
    /// <summary>
    /// Статус выполнения задания (например: "Назначено", "Выполнено", "Просрочено").
    /// </summary>
    public string Status { get; set; }
}
