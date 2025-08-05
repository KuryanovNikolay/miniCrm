using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.LessonDtos;
using WebApplication1.Repositories.LessonRepositories;

namespace WebApplication1.Services.LessonServices;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IMapper _mapper;

    public LessonService(ILessonRepository lessonRepository, IMapper mapper)
    {
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }

    public async Task<LessonDto> GetLessonByIdAsync(Guid id)
    {
        var lesson = await _lessonRepository.GetLessonByIdAsync(id);
        return _mapper.Map<LessonDto>(lesson);
    }

    public async Task<List<LessonDto>> GetAllLessonsAsync()
    {
        var lessons = await _lessonRepository.GetAllLessonsAsync();
        return _mapper.Map<List<LessonDto>>(lessons);
    }

    public async Task<LessonDto> CreateLessonAsync(CreateLessonDto lessonDto)
    {
        var lesson = _mapper.Map<Lesson>(lessonDto);
        var createdLesson = await _lessonRepository.CreateLessonAsync(lesson);
        return _mapper.Map<LessonDto>(createdLesson);
    }

    public async Task UpdateLessonAsync(Guid id, UpdateLessonDto lessonDto)
    {
        var lesson = await _lessonRepository.GetLessonByIdAsync(id);
        if (lesson == null) throw new KeyNotFoundException("Lesson not found");

        _mapper.Map(lessonDto, lesson);
        await _lessonRepository.UpdateLessonAsync(lesson);
    }

    public async Task DeleteLessonAsync(Guid id)
    {
        if (!await _lessonRepository.LessonExistsAsync(id))
            throw new KeyNotFoundException("Lesson not found");

        await _lessonRepository.DeleteLessonAsync(id);
    }

    public async Task<List<LessonDto>> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        var lessons = await _lessonRepository.GetLessonsByTeacherIdAsync(teacherId);
        return _mapper.Map<List<LessonDto>>(lessons);
    }

    public async Task<List<LessonDto>> GetLessonsByStudentIdAsync(Guid studentId)
    {
        var lessons = await _lessonRepository.GetLessonsByStudentIdAsync(studentId);
        return _mapper.Map<List<LessonDto>>(lessons);
    }
}