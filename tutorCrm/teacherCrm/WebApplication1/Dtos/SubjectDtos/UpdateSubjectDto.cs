using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubjectDtos;

/// <summary>
/// DTO для обновления информации о предмете.
/// </summary>
public class UpdateSubjectDto
{
    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    [Required]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Название предмета.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "Название предмета не должно превышать 100 символов")]
    public string Name { get; set; }

    /// <summary>
    /// Описание предмета (необязательное поле).
    /// </summary>
    [StringLength(500, ErrorMessage = "Описание предмета не должно превышать 500 символов")]
    public string? Description { get; set; }
}
