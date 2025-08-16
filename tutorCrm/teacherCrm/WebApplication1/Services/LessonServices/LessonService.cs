using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.LessonDtos;
using WebApplication1.Repositories.LessonRepositories;

namespace WebApplication1.Services.LessonServices;

/// <summary>
/// Сервис для работы с уроками.
/// Реализует бизнес-логику работы с уроками.
/// </summary>
public class LessonService : ILessonService
{
    /// <summary>
    /// Репозиторий для работы с уроками.
    /// </summary>
    private readonly ILessonRepository _lessonRepository;

    /// <summary>
    /// Объект для автоматического маппинга DTO и моделей.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса уроков.
    /// </summary>
    /// <param name="lessonRepository">Репозиторий уроков.</param>
    /// <param name="mapper">Объект маппинга.</param>
    public LessonService(ILessonRepository lessonRepository, IMapper mapper)
    {
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>DTO объекта урока.</returns>
    /// <exception cref="KeyNotFoundException">Если урок не найден.</exception>
    public async Task<LessonDto> GetLessonByIdAsync(Guid id)
    {
        var lesson = await _lessonRepository.GetLessonByIdAsync(id);
        return _mapper.Map<LessonDto>(lesson);
    }

    /// <summary>
    /// Получает список всех уроков.
    /// </summary>
    /// <returns>Список DTO объектов уроков.</returns>
    public async Task<List<LessonDto>> GetAllLessonsAsync()
    {
        var lessons = await _lessonRepository.GetAllLessonsAsync();
        return _mapper.Map<List<LessonDto>>(lessons);
    }

    /// <summary>
    /// Создает новый урок.
    /// </summary>
    /// <param name="lessonDto">DTO для создания урока.</param>
    /// <returns>DTO созданного урока.</returns>
    public async Task<LessonDto> CreateLessonAsync(CreateLessonDto lessonDto)
    {
        var lesson = _mapper.Map<Lesson>(lessonDto);
        var createdLesson = await _lessonRepository.CreateLessonAsync(lesson);
        return _mapper.Map<LessonDto>(createdLesson);
    }

    /// <summary>
    /// Обновляет существующий урок.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <param name="lessonDto">DTO с обновленными данными урока.</param>
    /// <exception cref="KeyNotFoundException">Если урок не найден.</exception>
    public async Task UpdateLessonAsync(Guid id, UpdateLessonDto lessonDto)
    {
        var lesson = await _lessonRepository.GetLessonByIdAsync(id);
        if (lesson == null) throw new KeyNotFoundException("Lesson not found");

        _mapper.Map(lessonDto, lesson);
        await _lessonRepository.UpdateLessonAsync(lesson);
    }

    /// <summary>
    /// Удаляет урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <exception cref="KeyNotFoundException">Если урок не найден.</exception>
    public async Task DeleteLessonAsync(Guid id)
    {
        if (!await _lessonRepository.LessonExistsAsync(id))
            throw new KeyNotFoundException("Lesson not found");

        await _lessonRepository.DeleteLessonAsync(id);
    }

    /// <summary>
    /// Получает уроки по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список DTO уроков преподавателя.</returns>
    public async Task<List<LessonDto>> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        var lessons = await _lessonRepository.GetLessonsByTeacherIdAsync(teacherId);
        return _mapper.Map<List<LessonDto>>(lessons);
    }

    /// <summary>
    /// Получает уроки по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список DTO уроков студента.</returns>
    public async Task<List<LessonDto>> GetLessonsByStudentIdAsync(Guid studentId)
    {
        var lessons = await _lessonRepository.GetLessonsByStudentIdAsync(studentId);
        return _mapper.Map<List<LessonDto>>(lessons);
    }
}