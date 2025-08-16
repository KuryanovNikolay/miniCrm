using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

/// <summary>
/// Урок, проводимый преподавателем для студента.
/// </summary>
public class Lesson
{
    /// <summary>
    /// Уникальный идентификатор урока.
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
    /// Идентификатор предмета урока.
    /// </summary>
    [Required]
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Дата и время проведения урока.
    /// </summary>
    [Required]
    public DateTime LessonDate { get; set; }

    /// <summary>
    /// Продолжительность урока в минутах (от 1 до 480).
    /// </summary>
    [Required]
    [Range(1, 480)]
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Тема урока (опционально).
    /// </summary>
    [StringLength(200)]
    public string Topic { get; set; }

    /// <summary>
    /// Статус урока (по умолчанию "Scheduled").
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Scheduled";

    /// <summary>
    /// Цена урока.
    /// </summary>
    [Range(0, 999999.99)]
    public decimal Price { get; set; }

    /// <summary>
    /// Заметки к уроку (опционально).
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// Преподаватель урока.
    /// </summary>
    public virtual ApplicationUser Teacher { get; set; }

    /// <summary>
    /// Студент урока.
    /// </summary>
    public virtual ApplicationUser Student { get; set; }

    /// <summary>
    /// Предмет урока.
    /// </summary>
    public virtual Subject Subject { get; set; }

    /// <summary>
    /// Список домашних заданий, связанных с уроком.
    /// </summary>
    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
}
