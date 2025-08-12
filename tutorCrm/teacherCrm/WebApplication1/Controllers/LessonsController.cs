using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.LessonDtos;
using WebApplication1.Services.LessonServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!User.IsInRole("Admin") &&
            lesson.TeacherId != userId &&
            lesson.StudentId != userId)
        {
            return Forbid();
        }

        return Ok(lesson);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLessons()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Admin"))
        {
            return Ok(await _lessonService.GetAllLessonsAsync());
        }
        else if (User.IsInRole("Teacher"))
        {
            return Ok(await _lessonService.GetLessonsByTeacherIdAsync(userId));
        }
        else if (User.IsInRole("Student"))
        {
            return Ok(await _lessonService.GetLessonsByStudentIdAsync(userId));
        }

        return Forbid();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && dto.TeacherId != userId)
            return Forbid();

        var createdLesson = await _lessonService.CreateLessonAsync(dto);
        return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] UpdateLessonDto dto)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && lesson.TeacherId != userId)
            return Forbid();

        await _lessonService.UpdateLessonAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && lesson.TeacherId != userId)
            return Forbid();

        await _lessonService.DeleteLessonAsync(id);
        return NoContent();
    }
}
