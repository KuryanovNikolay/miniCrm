using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.HomeworkDtos;
using WebApplication1.Services.HomeworkServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeworkController : ControllerBase
{
    private readonly IHomeworkService _homeworkService;

    public HomeworkController(IHomeworkService homeworkService)
    {
        _homeworkService = homeworkService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHomeworkById(Guid id)
    {
        var homework = await _homeworkService.GetHomeworkByIdAsync(id);
        return Ok(homework);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHomeworks()
    {
        var homeworks = await _homeworkService.GetAllHomeworksAsync();
        return Ok(homeworks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHomework([FromBody] CreateHomeworkDto dto)
    {
        var createdHomework = await _homeworkService.CreateHomeworkAsync(dto);
        return CreatedAtAction(nameof(GetHomeworkById), new { id = createdHomework.Id }, createdHomework);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHomework(Guid id, [FromBody] UpdateHomeworkDto dto)
    {
        await _homeworkService.UpdateHomeworkAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHomework(Guid id)
    {
        await _homeworkService.DeleteHomeworkAsync(id);
        return NoContent();
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetHomeworksByStudentId(Guid studentId)
    {
        var homeworks = await _homeworkService.GetHomeworksByStudentIdAsync(studentId);
        return Ok(homeworks);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetHomeworksByTeacherId(Guid teacherId)
    {
        var homeworks = await _homeworkService.GetHomeworksByTeacherIdAsync(teacherId);
        return Ok(homeworks);
    }
}