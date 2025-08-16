using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.HomeworkDtos;

/// <summary>
/// DTO для обновления существующего задания.
/// Все поля необязательные, можно менять частично.
/// </summary>
public class UpdateHomeworkDto
{
    /// <summary>
    /// Новый заголовок задания.
    /// </summary>
    [StringLength(200)]
    public string? Title { get; set; }
    /// <summary>
    /// Новое описание задания.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Новый дедлайн выполнения.
    /// </summary>
    public DateTime? Deadline { get; set; }
    /// <summary>
    /// Новый статус задания (например: "В процессе", "Выполнено").
    /// </summary>
    [StringLength(20)]
    public string? Status { get; set; }
}
