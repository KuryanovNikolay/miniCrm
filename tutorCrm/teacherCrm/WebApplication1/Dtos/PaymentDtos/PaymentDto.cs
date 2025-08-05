namespace WebApplication1.Dtos.PaymentDtos;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; }
    public Guid? LessonId { get; set; }
}