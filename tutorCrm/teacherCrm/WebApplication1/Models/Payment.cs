using System;
using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

/// <summary>
/// Модель платежа за урок.
/// </summary>
public class Payment
{
    /// <summary>
    /// Уникальный идентификатор платежа.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор преподавателя, которому производится платеж.
    /// </summary>
    [Required]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор студента, который осуществляет платеж.
    /// </summary>
    [Required]
    public Guid StudentId { get; set; }

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    [Required]
    [Range(0.01, 1000000)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Дата и время платежа (по UTC).
    /// </summary>
    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Статус платежа (например, "Pending", "Completed").
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    /// <summary>
    /// Идентификатор урока, за который произведен платеж (необязательный).
    /// </summary>
    public Guid? LessonId { get; set; }

    /// <summary>
    /// Навигационное свойство к преподавателю.
    /// </summary>
    public virtual ApplicationUser Teacher { get; set; }

    /// <summary>
    /// Навигационное свойство к студенту.
    /// </summary>
    public virtual ApplicationUser Student { get; set; }

    /// <summary>
    /// Навигационное свойство к уроку, за который произведен платеж.
    /// </summary>
    public virtual Lesson Lesson { get; set; }
}
