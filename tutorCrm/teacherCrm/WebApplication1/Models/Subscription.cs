using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

/// <summary>
/// Модель подписки студента на предмет с определенным преподавателем.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Уникальный идентификатор подписки.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    [Required]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор студента.
    /// </summary>
    [Required]
    public Guid StudentId { get; set; }

    /// <summary>
    /// Идентификатор предмета.
    /// </summary>
    [Required]
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Общее количество уроков в подписке.
    /// </summary>
    [Required]
    [Range(1, 100)]
    public int LessonsTotal { get; set; }

    /// <summary>
    /// Количество оставшихся уроков.
    /// </summary>
    [Required]
    [Range(0, 100)]
    public int LessonsLeft { get; set; }

    /// <summary>
    /// Общая стоимость подписки.
    /// </summary>
    [Required]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    /// <summary>
    /// Дата начала подписки.
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;

    /// <summary>
    /// Дата окончания подписки (необязательная).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Навигационное свойство к преподавателю.
    /// </summary>
    public virtual ApplicationUser Teacher { get; set; }

    /// <summary>
    /// Навигационное свойство к студенту.
    /// </summary>
    public virtual ApplicationUser Student { get; set; }

    /// <summary>
    /// Навигационное свойство к предмету.
    /// </summary>
    public virtual Subject Subject { get; set; }
}
