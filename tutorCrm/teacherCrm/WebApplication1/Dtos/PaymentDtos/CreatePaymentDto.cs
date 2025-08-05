using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.PaymentDtos;

public class CreatePaymentDto
{
    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public decimal Amount { get; set; }

    public Guid? LessonId { get; set; }
}