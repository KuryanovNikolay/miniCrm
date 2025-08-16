using System;
using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

/// <summary>
/// Домашнее задание для студента.
/// </summary>
public class Homework
{
    /// <summary>
    /// Уникальный идентификатор домашнего задания.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор урока, к которому относится задание.
    /// </summary>
    [Required]
    public Guid? LessonId { get; set; }

    /// <summary>
    /// Идентификатор преподавателя, назначившего задание.
    /// </summary>
    [Required]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор студента, которому назначено задание.
    /// </summary>
    [Required]
    public Guid StudentId { get; set; }

    /// <summary>
    /// Название домашнего задания.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Описание задания.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Срок выполнения задания (опционально).
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// Статус задания (по умолчанию "Assigned").
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Assigned";

    /// <summary>
    /// Связь с уроком.
    /// </summary>
    public virtual Lesson Lesson { get; set; }

    /// <summary>
    /// Преподаватель, назначивший задание.
    /// </summary>
    public virtual ApplicationUser Teacher { get; set; }

    /// <summary>
    /// Студент, которому назначено задание.
    /// </summary>
    public virtual ApplicationUser Student { get; set; }
}
