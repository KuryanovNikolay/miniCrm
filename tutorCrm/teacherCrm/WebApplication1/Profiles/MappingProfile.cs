using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.RolesDtos;

namespace WebApplication1.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг для ролей
        CreateMap<Role, RoleDto>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();

        // Добавьте другие маппинги по мере необходимости
        // Например, для пользователей:
        // CreateMap<User, UserDto>();
        // CreateMap<CreateUserDto, User>();
    }
}