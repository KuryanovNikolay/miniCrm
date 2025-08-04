using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.HomeworkDtos;
using WebApplication1.Dtos.RolesDtos;
using WebApplication1.Dtos.SubjectDtos;

namespace WebApplication1.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();

        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<UpdateSubjectDto, Subject>();

        CreateMap<Homework, HomeworkDto>();
        CreateMap<CreateHomeworkDto, Homework>();
        CreateMap<UpdateHomeworkDto, Homework>();
    }
}