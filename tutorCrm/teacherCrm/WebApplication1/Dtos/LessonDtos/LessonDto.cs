namespace WebApplication1.Dtos.LessonDtos;

public class LessonDto
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    public DateTime LessonDate { get; set; }
    public int DurationMinutes { get; set; }
    public string Topic { get; set; }
    public string Status { get; set; }
    public decimal Price { get; set; }
    public string Notes { get; set; }
}