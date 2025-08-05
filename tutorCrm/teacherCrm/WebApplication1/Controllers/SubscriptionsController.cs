using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.SubscriptionsDtos;
using WebApplication1.Services.SubscriptionServices;

namespace tutorCrm.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAll()
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubscriptionDto>> GetById(Guid id)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        return Ok(subscription);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetByStudentId(Guid studentId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByStudentIdAsync(studentId);
        return Ok(subscriptions);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetByTeacherId(Guid teacherId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByTeacherIdAsync(teacherId);
        return Ok(subscriptions);
    }

    [HttpPost]
    public async Task<ActionResult<SubscriptionDto>> Create(CreateSubscriptionDto createDto)
    {
        var subscription = await _subscriptionService.CreateSubscriptionAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSubscriptionDto updateDto)
    {
        try
        {
            var subscription = await _subscriptionService.UpdateSubscriptionAsync(id, updateDto);
            return Ok(subscription);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _subscriptionService.DeleteSubscriptionAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}