using System;

namespace WebApplication1.Dtos.PaymentDtos;

/// <summary>
/// DTO для отображения информации о платеже.
/// </summary>
public class PaymentDto
{
    /// <summary>
    /// Идентификатор платежа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор учителя.
    /// </summary>
    public Guid TeacherId { get; set; }

    /// <summary>
    /// Идентификатор студента.
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Дата и время платежа.
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Статус платежа.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Идентификатор урока, к которому относится платеж.
    /// </summary>
    public Guid? LessonId { get; set; }
}
