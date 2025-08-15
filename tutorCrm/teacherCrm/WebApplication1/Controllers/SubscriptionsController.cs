using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.SubscriptionsDtos;
using WebApplication1.Services.SubscriptionServices;

namespace tutorCrm.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAll()
    {
        if (!User.IsInRole("Admin"))
            return Forbid();

        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubscriptionDto>> GetById(Guid id)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
        if (subscription == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!User.IsInRole("Admin") &&
            subscription.TeacherId != userId &&
            subscription.StudentId != userId)
        {
            return Forbid();
        }

        return Ok(subscription);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetByStudentId(Guid studentId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Student") && userId != studentId)
            return Forbid();

        if (!User.IsInRole("Admin") && !User.IsInRole("Teacher") && !User.IsInRole("Student"))
            return Forbid();

        var subscriptions = await _subscriptionService.GetSubscriptionsByStudentIdAsync(studentId);
        return Ok(subscriptions);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetByTeacherId(Guid teacherId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && userId != teacherId)
            return Forbid();

        if (!User.IsInRole("Admin") && !User.IsInRole("Teacher"))
            return Forbid();

        var subscriptions = await _subscriptionService.GetSubscriptionsByTeacherIdAsync(teacherId);
        return Ok(subscriptions);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<ActionResult<SubscriptionDto>> Create(CreateSubscriptionDto createDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && createDto.TeacherId != userId)
            return Forbid();

        var subscription = await _subscriptionService.CreateSubscriptionAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Update(Guid id, UpdateSubscriptionDto updateDto)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
        if (subscription == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && subscription.TeacherId != userId)
            return Forbid();

        var updatedSubscription = await _subscriptionService.UpdateSubscriptionAsync(id, updateDto);
        return Ok(updatedSubscription);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
        if (subscription == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && subscription.TeacherId != userId)
            return Forbid();

        await _subscriptionService.DeleteSubscriptionAsync(id);
        return NoContent();
    }
}
