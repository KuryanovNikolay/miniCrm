using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.HomeworkDtos;
using WebApplication1.Services.HomeworkServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]

/// <summary>
/// Контроллер для управления домашними заданиями.
/// </summary>
public class HomeworkController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с домашними заданиями.
    /// </summary>
    private readonly IHomeworkService _homeworkService;

    /// <summary>
    /// Конструктор контроллера домашнего задания.
    /// </summary>
    /// <param name="homeworkService">Сервис для управления домашними заданиями.</param>

    public HomeworkController(IHomeworkService homeworkService)
    {
        _homeworkService = homeworkService;
    }

    /// <summary>
    /// Получает домашнее задание по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор домашнего задания.</param>
    /// <returns>Объект домашнего задания.</returns>
    [Authorize(Roles = "Admin,Student,Parent,User,Teacher")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHomeworkById(Guid id)
    {
        var homework = await _homeworkService.GetHomeworkByIdAsync(id);
        return Ok(homework);
    }

    /// <summary>
    /// Получает список всех домашних заданий.
    /// </summary>
    /// <returns>Список домашних заданий.</returns>
    [Authorize(Roles = "Admin,User,Teacher")]
    [HttpGet]
    public async Task<IActionResult> GetAllHomeworks()
    {
        var homeworks = await _homeworkService.GetAllHomeworksAsync();
        return Ok(homeworks);
    }

    /// <summary>
    /// Создает новое домашнее задание.
    /// </summary>
    /// <param name="dto">Данные для создания домашнего задания.</param>
    /// <returns>Созданное домашнее задание.</returns>
    [Authorize(Roles = "Admin,Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateHomework([FromBody] CreateHomeworkDto dto)
    {
        var createdHomework = await _homeworkService.CreateHomeworkAsync(dto);
        return CreatedAtAction(nameof(GetHomeworkById), new { id = createdHomework.Id }, createdHomework);
    }

    /// <summary>
    /// Обновляет существующее домашнее задание.
    /// </summary>
    /// <param name="id">Идентификатор домашнего задания.</param>
    /// <param name="dto">Обновленные данные домашнего задания.</param>
    /// <returns>HTTP 204 (No Content) при успешном обновлении.</returns>
    [Authorize(Roles = "Admin,Teacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHomework(Guid id, [FromBody] UpdateHomeworkDto dto)
    {
        await _homeworkService.UpdateHomeworkAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет домашнее задание.
    /// </summary>
    /// <param name="id">Идентификатор домашнего задания.</param>
    /// <returns>HTTP 204 (No Content) при успешном удалении.</returns>
    [Authorize(Roles = "Admin,Teacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHomework(Guid id)
    {
        await _homeworkService.DeleteHomeworkAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Получает список домашних заданий по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список домашних заданий студента.</returns>
    [Authorize(Roles = "Admin,Student,Teacher")]
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetHomeworksByStudentId(Guid studentId)
    {
        var homeworks = await _homeworkService.GetHomeworksByStudentIdAsync(studentId);
        return Ok(homeworks);
    }

    /// <summary>
    /// Получает список домашних заданий по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список домашних заданий преподавателя.</returns>
    [Authorize(Roles = "Admin,Parent,Teacher")]
    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetHomeworksByTeacherId(Guid teacherId)
    {
        var homeworks = await _homeworkService.GetHomeworksByTeacherIdAsync(teacherId);
        return Ok(homeworks);
    }
}
