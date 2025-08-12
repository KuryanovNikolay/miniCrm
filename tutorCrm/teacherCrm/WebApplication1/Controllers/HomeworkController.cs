using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin,Student,Parent,User,Teacher")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHomeworkById(Guid id)
    {
        var homework = await _homeworkService.GetHomeworkByIdAsync(id);
        return Ok(homework);
    }

    [Authorize(Roles = "Admin,User,Teacher")]
    [HttpGet]
    public async Task<IActionResult> GetAllHomeworks()
    {
        var homeworks = await _homeworkService.GetAllHomeworksAsync();
        return Ok(homeworks);
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateHomework([FromBody] CreateHomeworkDto dto)
    {
        var createdHomework = await _homeworkService.CreateHomeworkAsync(dto);
        return CreatedAtAction(nameof(GetHomeworkById), new { id = createdHomework.Id }, createdHomework);
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHomework(Guid id, [FromBody] UpdateHomeworkDto dto)
    {
        await _homeworkService.UpdateHomeworkAsync(id, dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHomework(Guid id)
    {
        await _homeworkService.DeleteHomeworkAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin,Student,Teacher")]
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetHomeworksByStudentId(Guid studentId)
    {
        var homeworks = await _homeworkService.GetHomeworksByStudentIdAsync(studentId);
        return Ok(homeworks);
    }

    [Authorize(Roles = "Admin,Parent,Teacher")]
    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetHomeworksByTeacherId(Guid teacherId)
    {
        var homeworks = await _homeworkService.GetHomeworksByTeacherIdAsync(teacherId);
        return Ok(homeworks);
    }
}
