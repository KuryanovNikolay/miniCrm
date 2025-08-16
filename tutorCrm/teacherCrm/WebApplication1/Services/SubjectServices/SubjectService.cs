using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.SubjectDtos;
using WebApplication1.Repositories.SubjectRepositories;

namespace WebApplication1.Services.SubjectServices;

/// <summary>
/// Сервис для работы с учебными предметами.
/// Реализует бизнес-логику управления предметами.
/// </summary>
public class SubjectService : ISubjectService
{
    /// <summary>
    /// Репозиторий для работы с предметами в базе данных.
    /// </summary>
    private readonly ISubjectRepository _subjectRepository;

    /// <summary>
    /// Объект для автоматического маппинга между DTO и моделями.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса предметов.
    /// </summary>
    /// <param name="subjectRepository">Репозиторий предметов.</param>
    /// <param name="mapper">Объект для преобразования моделей.</param>
    public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает предмет по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>DTO объекта предмета.</returns>
    /// <exception cref="KeyNotFoundException">Если предмет не найден.</exception>
    public async Task<SubjectDto> GetSubjectByIdAsync(Guid id)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(id);
        return _mapper.Map<SubjectDto>(subject);
    }

    /// <summary>
    /// Получает список всех учебных предметов.
    /// </summary>
    /// <returns>Список DTO объектов предметов.</returns>
    public async Task<List<SubjectDto>> GetAllSubjectsAsync()
    {
        var subjects = await _subjectRepository.GetAllSubjectsAsync();
        return _mapper.Map<List<SubjectDto>>(subjects);
    }

    /// <summary>
    /// Создает новый учебный предмет.
    /// </summary>
    /// <param name="subjectDto">DTO с данными для создания предмета.</param>
    /// <returns>DTO созданного предмета.</returns>
    public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto)
    {
        var subject = _mapper.Map<Subject>(subjectDto);
        var createdSubject = await _subjectRepository.CreateSubjectAsync(subject);
        return _mapper.Map<SubjectDto>(createdSubject);
    }

    /// <summary>
    /// Обновляет существующий учебный предмет.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого предмета.</param>
    /// <param name="subjectDto">DTO с обновленными данными предмета.</param>
    /// <exception cref="KeyNotFoundException">Если предмет не найден.</exception>
    public async Task UpdateSubjectAsync(Guid id, UpdateSubjectDto subjectDto)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(id);
        if (subject == null) throw new KeyNotFoundException("Subject not found");

        _mapper.Map(subjectDto, subject);
        await _subjectRepository.UpdateSubjectAsync(subject);
    }

    /// <summary>
    /// Удаляет учебный предмет по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого предмета.</param>
    /// <exception cref="KeyNotFoundException">Если предмет не найден.</exception>
    public async Task DeleteSubjectAsync(Guid id)
    {
        if (!await _subjectRepository.SubjectExistsAsync(id))
            throw new KeyNotFoundException("Subject not found");

        await _subjectRepository.DeleteSubjectAsync(id);
    }
}