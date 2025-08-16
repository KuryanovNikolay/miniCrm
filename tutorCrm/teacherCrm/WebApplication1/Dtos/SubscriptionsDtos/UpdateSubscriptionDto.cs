using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubscriptionsDtos;

/// <summary>
/// DTO для обновления подписки.
/// </summary>
public class UpdateSubscriptionDto
{
    /// <summary>
    /// Общее количество уроков в подписке (необязательное поле).
    /// </summary>
    [Range(1, 100)]
    public int? LessonsTotal { get; set; }

    /// <summary>
    /// Количество оставшихся уроков (необязательное поле).
    /// </summary>
    [Range(0, 100)]
    public int? LessonsLeft { get; set; }

    /// <summary>
    /// Цена подписки (необязательное поле).
    /// </summary>
    [Range(0.01, 1000000)]
    public decimal? Price { get; set; }

    /// <summary>
    /// Дата окончания подписки (необязательное поле).
    /// </summary>
    public DateTime? EndDate { get; set; }
}
