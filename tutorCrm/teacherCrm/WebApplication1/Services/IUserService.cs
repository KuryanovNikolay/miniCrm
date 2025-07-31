using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services;

public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<List<User>> GetAllUsersAsync();
        Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task<bool> IsUserInRoleAsync(Guid userId, string roleName);
        Task<bool> IsStudentAsync(Guid userId);
        Task<bool> IsTeacherAsync(Guid userId);
}

