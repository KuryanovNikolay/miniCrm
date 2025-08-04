using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.SubjectDtos;
using WebApplication1.Services.SubjectServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(Guid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        return Ok(subject);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubjects()
    {
        var subjects = await _subjectService.GetAllSubjectsAsync();
        return Ok(subjects);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto dto)
    {
        var createdSubject = await _subjectService.CreateSubjectAsync(dto);
        return CreatedAtAction(nameof(GetSubjectById), new { id = createdSubject.Id }, createdSubject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] UpdateSubjectDto dto)
    {
        await _subjectService.UpdateSubjectAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        await _subjectService.DeleteSubjectAsync(id);
        return NoContent();
    }
}