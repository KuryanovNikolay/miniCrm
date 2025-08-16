using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.PaymentDtos;

/// <summary>
/// DTO для создания нового платежа.
/// </summary>
public class CreatePaymentDto
{
    /// <summary>
    /// Идентификатор учителя.
    /// </summary>
    [Required(ErrorMessage = "TeacherId обязателен")]
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор студента.
    /// </summary>
    [Required(ErrorMessage = "StudentId обязателен")]
    public Guid StudentId { get; set; }

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    [Required(ErrorMessage = "Amount обязателен")]
    [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть от 0.01 до 1 000 000")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Необязательный идентификатор урока, к которому относится платеж.
    /// </summary>
    public Guid? LessonId { get; set; }
}
