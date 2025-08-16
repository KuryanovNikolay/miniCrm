using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.LessonDtos;

/// <summary>
/// DTO для обновления урока.
/// Все поля необязательные — можно изменять частично.
/// </summary>
public class UpdateLessonDto
{
    /// <summary>
    /// Новая дата и время урока.
    /// </summary>
    public DateTime? LessonDate { get; set; }

    /// <summary>
    /// Новая продолжительность урока (1–480 минут).
    /// </summary>
    [Range(1, 480)]
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Новая тема урока.
    /// </summary>
    [StringLength(200)]
    public string Topic { get; set; }

    /// <summary>
    /// Новый статус урока.
    /// </summary>
    [StringLength(20)]
    public string Status { get; set; }

    /// <summary>
    /// Новая цена за урок.
    /// </summary>
    [Range(0, 999999.99)]
    public decimal? Price { get; set; }

    /// <summary>
    /// Новые заметки.
    /// </summary>
    public string Notes { get; set; }
}
