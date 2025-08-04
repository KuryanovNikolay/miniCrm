using tutorCrm.Models;

namespace WebApplication1.Repositories.HomeworkRepositories;

public interface IHomeworkRepository
{
    Task<Homework?> GetHomeworkByIdAsync(Guid id);
    Task<List<Homework>> GetAllHomeworksAsync();
    Task<Homework> CreateHomeworkAsync(Homework homework);
    Task UpdateHomeworkAsync(Homework homework);
    Task<bool> HomeworkExistsAsync(Guid id);
    Task DeleteHomeworkAsync(Guid id);
    Task<List<Homework>> GetHomeworksByStudentIdAsync(Guid studentId);
    Task<List<Homework>> GetHomeworksByTeacherIdAsync(Guid teacherId);
}