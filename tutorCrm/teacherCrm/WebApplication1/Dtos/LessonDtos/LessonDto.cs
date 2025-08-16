namespace WebApplication1.Dtos.LessonDtos;

/// <summary>
/// DTO для отображения информации об уроке.
/// Используется при возврате данных клиенту.
/// </summary>
public class LessonDto
{
    /// <summary>
    /// Уникальный идентификатор урока.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор студента.
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Идентификатор предмета.
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Дата и время проведения урока.
    /// </summary>
    public DateTime LessonDate { get; set; }

    /// <summary>
    /// Продолжительность урока в минутах.
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Тема урока.
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// Статус урока (например: "Запланирован", "Завершён", "Отменён").
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Цена за урок.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Дополнительные заметки.
    /// </summary>
    public string Notes { get; set; }
}
