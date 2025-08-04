using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.HomeworkDtos;
using WebApplication1.Repositories.HomeworkRepositories;

namespace WebApplication1.Services.HomeworkServices;

public class HomeworkService : IHomeworkService
{
    private readonly IHomeworkRepository _homeworkRepository;
    private readonly IMapper _mapper;

    public HomeworkService(IHomeworkRepository homeworkRepository, IMapper mapper)
    {
        _homeworkRepository = homeworkRepository;
        _mapper = mapper;
    }

    public async Task<HomeworkDto> GetHomeworkByIdAsync(Guid id)
    {
        var homework = await _homeworkRepository.GetHomeworkByIdAsync(id);
        return _mapper.Map<HomeworkDto>(homework);
    }

    public async Task<List<HomeworkDto>> GetAllHomeworksAsync()
    {
        var homeworks = await _homeworkRepository.GetAllHomeworksAsync();
        return _mapper.Map<List<HomeworkDto>>(homeworks);
    }

    public async Task<HomeworkDto> CreateHomeworkAsync(CreateHomeworkDto homeworkDto)
    {
        var homework = _mapper.Map<Homework>(homeworkDto);
        homework.Status = "Assigned"; // Устанавливаем статус по умолчанию
        var createdHomework = await _homeworkRepository.CreateHomeworkAsync(homework);
        return _mapper.Map<HomeworkDto>(createdHomework);
    }

    public async Task UpdateHomeworkAsync(Guid id, UpdateHomeworkDto homeworkDto)
    {
        var homework = await _homeworkRepository.GetHomeworkByIdAsync(id);
        if (homework == null) throw new KeyNotFoundException("Homework not found");

        _mapper.Map(homeworkDto, homework);
        await _homeworkRepository.UpdateHomeworkAsync(homework);
    }

    public async Task DeleteHomeworkAsync(Guid id)
    {
        if (!await _homeworkRepository.HomeworkExistsAsync(id))
            throw new KeyNotFoundException("Homework not found");

        await _homeworkRepository.DeleteHomeworkAsync(id);
    }

    public async Task<List<HomeworkDto>> GetHomeworksByStudentIdAsync(Guid studentId)
    {
        var homeworks = await _homeworkRepository.GetHomeworksByStudentIdAsync(studentId);
        return _mapper.Map<List<HomeworkDto>>(homeworks);
    }

    public async Task<List<HomeworkDto>> GetHomeworksByTeacherIdAsync(Guid teacherId)
    {
        var homeworks = await _homeworkRepository.GetHomeworksByTeacherIdAsync(teacherId);
        return _mapper.Map<List<HomeworkDto>>(homeworks);
    }
}