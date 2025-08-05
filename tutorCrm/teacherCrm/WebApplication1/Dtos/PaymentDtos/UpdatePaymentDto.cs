using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.PaymentDtos;

public class UpdatePaymentDto
{
    [Range(0.01, 1000000)]
    public decimal? Amount { get; set; }

    [StringLength(20)]
    public string Status { get; set; }
}
