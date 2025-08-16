using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.HomeworkDtos;
using WebApplication1.Repositories.HomeworkRepositories;

namespace WebApplication1.Services.HomeworkServices;

/// <summary>
/// Сервис для работы с домашними заданиями.
/// Обеспечивает бизнес-логику работы с домашними заданиями.
/// </summary>
public class HomeworkService : IHomeworkService
{
    /// <summary>
    /// Репозиторий для работы с домашними заданиями в базе данных.
    /// </summary>
    private readonly IHomeworkRepository _homeworkRepository;

    /// <summary>
    /// Объект для автоматического преобразования между DTO и моделями.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="HomeworkService"/>.
    /// </summary>
    /// <param name="homeworkRepository">Репозиторий домашних заданий.</param>
    /// <param name="mapper">Объект маппинга.</param>
    public HomeworkService(IHomeworkRepository homeworkRepository, IMapper mapper)
    {
        _homeworkRepository = homeworkRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает домашнее задание по указанному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор домашнего задания.</param>
    /// <returns>DTO объект с данными домашнего задания.</returns>
    /// <exception cref="KeyNotFoundException">Если задание не найдено.</exception>
    public async Task<HomeworkDto> GetHomeworkByIdAsync(Guid id)
    {
        var homework = await _homeworkRepository.GetHomeworkByIdAsync(id);
        return _mapper.Map<HomeworkDto>(homework);
    }

    /// <summary>
    /// Получает список всех домашних заданий в системе.
    /// </summary>
    /// <returns>Коллекция DTO объектов домашних заданий.</returns>
    public async Task<List<HomeworkDto>> GetAllHomeworksAsync()
    {
        var homeworks = await _homeworkRepository.GetAllHomeworksAsync();
        return _mapper.Map<List<HomeworkDto>>(homeworks);
    }

    /// <summary>
    /// Создает новое домашнее задание.
    /// </summary>
    /// <param name="homeworkDto">DTO с данными для создания задания.</param>
    /// <returns>DTO созданного домашнего задания.</returns>
    public async Task<HomeworkDto> CreateHomeworkAsync(CreateHomeworkDto homeworkDto)
    {
        var homework = _mapper.Map<Homework>(homeworkDto);
        homework.Status = "Assigned";
        var createdHomework = await _homeworkRepository.CreateHomeworkAsync(homework);
        return _mapper.Map<HomeworkDto>(createdHomework);
    }

    /// <summary>
    /// Обновляет существующее домашнее задание.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого задания.</param>
    /// <param name="homeworkDto">DTO с обновленными данными.</param>
    /// <exception cref="KeyNotFoundException">Если задание не найдено.</exception>
    public async Task UpdateHomeworkAsync(Guid id, UpdateHomeworkDto homeworkDto)
    {
        var homework = await _homeworkRepository.GetHomeworkByIdAsync(id);
        if (homework == null)
            throw new KeyNotFoundException("Homework not found");

        _mapper.Map(homeworkDto, homework);
        await _homeworkRepository.UpdateHomeworkAsync(homework);
    }

    /// <summary>
    /// Удаляет домашнее задание по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого задания.</param>
    /// <exception cref="KeyNotFoundException">Если задание не найдено.</exception>
    public async Task DeleteHomeworkAsync(Guid id)
    {
        if (!await _homeworkRepository.HomeworkExistsAsync(id))
            throw new KeyNotFoundException("Homework not found");

        await _homeworkRepository.DeleteHomeworkAsync(id);
    }

    /// <summary>
    /// Получает домашние задания для указанного студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Коллекция DTO заданий студента.</returns>
    public async Task<List<HomeworkDto>> GetHomeworksByStudentIdAsync(Guid studentId)
    {
        var homeworks = await _homeworkRepository.GetHomeworksByStudentIdAsync(studentId);
        return _mapper.Map<List<HomeworkDto>>(homeworks);
    }

    /// <summary>
    /// Получает домашние задания для указанного преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Коллекция DTO заданий преподавателя.</returns>
    public async Task<List<HomeworkDto>> GetHomeworksByTeacherIdAsync(Guid teacherId)
    {
        var homeworks = await _homeworkRepository.GetHomeworksByTeacherIdAsync(teacherId);
        return _mapper.Map<List<HomeworkDto>>(homeworks);
    }
}