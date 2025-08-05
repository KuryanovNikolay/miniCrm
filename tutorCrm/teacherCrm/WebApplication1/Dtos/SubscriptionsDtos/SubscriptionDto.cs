namespace WebApplication1.Dtos.SubscriptionsDtos;

public class SubscriptionDto
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    public int LessonsTotal { get; set; }
    public int LessonsLeft { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
