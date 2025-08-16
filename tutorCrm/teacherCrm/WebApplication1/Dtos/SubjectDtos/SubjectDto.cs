using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubjectDtos;

/// <summary>
/// DTO для отображения информации о предмете.
/// </summary>
public class SubjectDto
{
    /// <summary>
    /// Идентификатор предмета.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название предмета.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание предмета (необязательное поле).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Идентификатор преподавателя.
    /// </summary>
    [Required]
    public Guid TeacherId { get; set; }
}
