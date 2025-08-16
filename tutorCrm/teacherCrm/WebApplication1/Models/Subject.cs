using System.ComponentModel.DataAnnotations;

namespace tutorCrm.Models;

/// <summary>
/// Модель предмета, который преподается.
/// </summary>
public class Subject
{
    /// <summary>
    /// Уникальный идентификатор предмета.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название предмета.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Описание предмета (необязательное).
    /// </summary>
    [StringLength(500)]
    public string Description { get; set; }

    /// <summary>
    /// Список уроков, связанных с данным предметом.
    /// </summary>
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
