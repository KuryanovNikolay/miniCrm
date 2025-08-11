using Microsoft.AspNetCore.Mvc;
using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUser>> GetUser(Guid id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ApplicationUser>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        try
        {
            var user = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] ApplicationUser user)
    {
        try
        {
            if (id != user.Id)
            {
                return BadRequest("ID mismatch");
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
