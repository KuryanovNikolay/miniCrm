using WebApplication1.Dtos.LessonDtos;

namespace WebApplication1.Services.LessonServices;

public interface ILessonService
{
    Task<LessonDto> GetLessonByIdAsync(Guid id);
    Task<List<LessonDto>> GetAllLessonsAsync();
    Task<LessonDto> CreateLessonAsync(CreateLessonDto lessonDto);
    Task UpdateLessonAsync(Guid id, UpdateLessonDto lessonDto);
    Task DeleteLessonAsync(Guid id);
    Task<List<LessonDto>> GetLessonsByTeacherIdAsync(Guid teacherId);
    Task<List<LessonDto>> GetLessonsByStudentIdAsync(Guid studentId);
}