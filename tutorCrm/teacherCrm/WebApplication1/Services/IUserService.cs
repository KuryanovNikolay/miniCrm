using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services;

public interface IUserService
    {
        Task<ApplicationUser?> GetUserByIdAsync(Guid id);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(Guid id);
}

