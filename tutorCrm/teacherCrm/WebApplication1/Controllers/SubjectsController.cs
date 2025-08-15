using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.SubjectDtos;
using WebApplication1.Services.SubjectServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        if (subject == null)
            return NotFound();

        return Ok(subject);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllSubjects()
    {
        var subjects = await _subjectService.GetAllSubjectsAsync();
        return Ok(subjects);
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (dto.TeacherId != userId)
            return Forbid();

        var createdSubject = await _subjectService.CreateSubjectAsync(dto);
        return CreatedAtAction(nameof(GetSubjectById), new { id = createdSubject.Id }, createdSubject);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] UpdateSubjectDto dto)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && subject.TeacherId != userId)
            return Forbid();

        await _subjectService.UpdateSubjectAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && subject.TeacherId != userId)
            return Forbid();

        await _subjectService.DeleteSubjectAsync(id);
        return NoContent();
    }
}
