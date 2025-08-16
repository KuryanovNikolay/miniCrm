using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.PaymentDtos;

/// <summary>
/// DTO для обновления информации о платеже.
/// </summary>
public class UpdatePaymentDto
{
    /// <summary>
    /// Сумма платежа (необязательное поле для обновления).
    /// </summary>
    [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть от 0.01 до 1 000 000")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Статус платежа (необязательное поле для обновления).
    /// </summary>
    [StringLength(20, ErrorMessage = "Статус не должен превышать 20 символов")]
    public string Status { get; set; }
}
