using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubscriptionsDtos;

/// <summary>
/// DTO для создания новой подписки.
/// </summary>
public class CreateSubscriptionDto
{
    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    [Required]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор ученика.
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
    /// Цена подписки.
    /// </summary>
    [Required]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    /// <summary>
    /// Дата начала подписки (необязательное поле).
    /// </summary>
    public DateTime? StartDate { get; set; }
}
