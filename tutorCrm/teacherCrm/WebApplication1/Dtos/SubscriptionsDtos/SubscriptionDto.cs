namespace WebApplication1.Dtos.SubscriptionsDtos;

/// <summary>
/// DTO для отображения информации о подписке.
/// </summary>
public class SubscriptionDto
{
    /// <summary>
    /// Идентификатор подписки.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор ученика.
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Идентификатор предмета.
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Общее количество уроков в подписке.
    /// </summary>
    public int LessonsTotal { get; set; }

    /// <summary>
    /// Количество оставшихся уроков.
    /// </summary>
    public int LessonsLeft { get; set; }

    /// <summary>
    /// Цена подписки.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Дата начала подписки.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания подписки (необязательное поле).
    /// </summary>
    public DateTime? EndDate { get; set; }
}
