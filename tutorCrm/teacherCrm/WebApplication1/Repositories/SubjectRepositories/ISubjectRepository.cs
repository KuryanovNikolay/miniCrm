using tutorCrm.Models;

namespace WebApplication1.Repositories.SubjectRepositories;

public interface ISubjectRepository
{
    Task<Subject?> GetSubjectByIdAsync(Guid id);
    Task<List<Subject>> GetAllSubjectsAsync();
    Task<Subject> CreateSubjectAsync(Subject subject);
    Task UpdateSubjectAsync(Subject subject);
    Task<bool> SubjectExistsAsync(Guid id);
    Task DeleteSubjectAsync(Guid id);
}