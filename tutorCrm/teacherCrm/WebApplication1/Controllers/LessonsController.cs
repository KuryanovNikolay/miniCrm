using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.LessonDtos;
using WebApplication1.Services.LessonServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return Ok(lesson);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLessons()
    {
        var lessons = await _lessonService.GetAllLessonsAsync();
        return Ok(lessons);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto dto)
    {
        var createdLesson = await _lessonService.CreateLessonAsync(dto);
        return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] UpdateLessonDto dto)
    {
        await _lessonService.UpdateLessonAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        await _lessonService.DeleteLessonAsync(id);
        return NoContent();
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetLessonsByTeacherId(Guid teacherId)
    {
        var lessons = await _lessonService.GetLessonsByTeacherIdAsync(teacherId);
        return Ok(lessons);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetLessonsByStudentId(Guid studentId)
    {
        var lessons = await _lessonService.GetLessonsByStudentIdAsync(studentId);
        return Ok(lessons);
    }
}