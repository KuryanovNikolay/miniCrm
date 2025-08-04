using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto)
    {
        if (await _userRepository.UserExistsAsync(userDto.Username, userDto.Email))
        {
            throw new InvalidOperationException("User already exists");
        }

        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email,
            FullName = userDto.FullName,
            PhoneNumber = userDto.PhoneNumber,
            ParentFullName = userDto.ParentFullName,
            ParentContact = userDto.ParentContact,
            RegistrationDate = DateTime.UtcNow
        };

        await _userRepository.AddUserAsync(user);
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            RegistrationDate = user.RegistrationDate,
            ParentFullName = user.ParentFullName,
            ParentContact = user.ParentContact
        };
    }

    public async Task UpdateUserAsync(User user)
    {
        User? existingUser = await _userRepository.GetUserByIdAsync(user.Id);

        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        User? user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
    {
        return await _userRepository.IsUserInRoleAsync(userId, roleName);
    }

    public async Task<bool> IsStudentAsync(Guid userId)
    {
        return await IsUserInRoleAsync(userId, "Student");
    }

    public async Task<bool> IsTeacherAsync(Guid userId) 
    {
        return await IsUserInRoleAsync(userId, "Teacher");
    }
}
