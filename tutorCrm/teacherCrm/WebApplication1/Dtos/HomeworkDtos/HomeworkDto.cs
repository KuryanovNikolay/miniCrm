namespace WebApplication1.Dtos.HomeworkDtos;

public class HomeworkDto
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Deadline { get; set; }
    public string Status { get; set; }
}