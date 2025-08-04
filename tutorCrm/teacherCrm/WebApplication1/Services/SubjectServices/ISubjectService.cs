using WebApplication1.Dtos.SubjectDtos;

namespace WebApplication1.Services.SubjectServices;

public interface ISubjectService
{
    Task<SubjectDto> GetSubjectByIdAsync(Guid id);
    Task<List<SubjectDto>> GetAllSubjectsAsync();
    Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto);
    Task UpdateSubjectAsync(Guid id, UpdateSubjectDto subjectDto);
    Task DeleteSubjectAsync(Guid id);
}