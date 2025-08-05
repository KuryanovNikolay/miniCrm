using tutorCrm.Models;

namespace WebApplication1.Repositories.LessonRepositories;

public interface ILessonRepository
{
    Task<Lesson?> GetLessonByIdAsync(Guid id);
    Task<List<Lesson>> GetAllLessonsAsync();
    Task<Lesson> CreateLessonAsync(Lesson lesson);
    Task UpdateLessonAsync(Lesson lesson);
    Task<bool> LessonExistsAsync(Guid id);
    Task DeleteLessonAsync(Guid id);
    Task<List<Lesson>> GetLessonsByTeacherIdAsync(Guid teacherId);
    Task<List<Lesson>> GetLessonsByStudentIdAsync(Guid studentId);
}