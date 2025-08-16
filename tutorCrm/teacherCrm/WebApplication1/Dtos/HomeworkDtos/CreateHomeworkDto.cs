using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.HomeworkDtos;

/// <summary>
/// DTO для создания нового задания.
/// </summary>
public class CreateHomeworkDto
{
    /// <summary>
    /// Идентификатор урока, к которому относится задание.
    /// </summary>
    [Required]
    public Guid LessonId { get; set; }
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
    /// Краткое название задания.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Описание задания.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Дедлайн выполнения задания (необязательный).
    /// </summary>

    public DateTime? Deadline { get; set; }
}