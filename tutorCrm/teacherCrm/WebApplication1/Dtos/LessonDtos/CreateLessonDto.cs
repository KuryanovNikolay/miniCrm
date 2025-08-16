using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.LessonDtos;

/// <summary>
/// DTO для создания нового урока.
/// </summary>
public class CreateLessonDto
{
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
    /// Дата и время проведения урока.
    /// </summary>
    [Required]
    public DateTime LessonDate { get; set; }

    /// <summary>
    /// Продолжительность урока в минутах (1–480).
    /// </summary>
    [Required]
    [Range(1, 480)]
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Тема урока.
    /// </summary>
    [StringLength(200)]
    public string Topic { get; set; }

    /// <summary>
    /// Цена за урок.
    /// </summary>
    [Range(0, 999999.99)]
    public decimal Price { get; set; }

    /// <summary>
    /// Дополнительные заметки.
    /// </summary>
    public string Notes { get; set; }
}
