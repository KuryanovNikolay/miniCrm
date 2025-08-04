using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.SubjectDtos;
using WebApplication1.Repositories.SubjectRepositories;

namespace WebApplication1.Services.SubjectServices;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<SubjectDto> GetSubjectByIdAsync(Guid id)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(id);
        return _mapper.Map<SubjectDto>(subject);
    }

    public async Task<List<SubjectDto>> GetAllSubjectsAsync()
    {
        var subjects = await _subjectRepository.GetAllSubjectsAsync();
        return _mapper.Map<List<SubjectDto>>(subjects);
    }

    public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto)
    {
        var subject = _mapper.Map<Subject>(subjectDto);
        var createdSubject = await _subjectRepository.CreateSubjectAsync(subject);
        return _mapper.Map<SubjectDto>(createdSubject);
    }

    public async Task UpdateSubjectAsync(Guid id, UpdateSubjectDto subjectDto)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(id);
        if (subject == null) throw new KeyNotFoundException("Subject not found");

        _mapper.Map(subjectDto, subject);
        await _subjectRepository.UpdateSubjectAsync(subject);
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        if (!await _subjectRepository.SubjectExistsAsync(id))
            throw new KeyNotFoundException("Subject not found");

        await _subjectRepository.DeleteSubjectAsync(id);
    }
}