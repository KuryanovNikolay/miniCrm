using WebApplication1.Dtos.HomeworkDtos;

namespace WebApplication1.Services.HomeworkServices;

public interface IHomeworkService
{
    Task<HomeworkDto> GetHomeworkByIdAsync(Guid id);
    Task<List<HomeworkDto>> GetAllHomeworksAsync();
    Task<HomeworkDto> CreateHomeworkAsync(CreateHomeworkDto homeworkDto);
    Task UpdateHomeworkAsync(Guid id, UpdateHomeworkDto homeworkDto);
    Task DeleteHomeworkAsync(Guid id);
    Task<List<HomeworkDto>> GetHomeworksByStudentIdAsync(Guid studentId);
    Task<List<HomeworkDto>> GetHomeworksByTeacherIdAsync(Guid teacherId);
}