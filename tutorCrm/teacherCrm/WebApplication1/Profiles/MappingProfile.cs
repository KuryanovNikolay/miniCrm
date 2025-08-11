using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.HomeworkDtos;
using WebApplication1.Dtos.LessonDtos;
using WebApplication1.Dtos.PaymentDtos;
using WebApplication1.Dtos.RolesDtos;
using WebApplication1.Dtos.SubjectDtos;
using WebApplication1.Dtos.SubscriptionsDtos;

namespace WebApplication1.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<UpdateSubjectDto, Subject>();

        CreateMap<Homework, HomeworkDto>();
        CreateMap<CreateHomeworkDto, Homework>();
        CreateMap<UpdateHomeworkDto, Homework>();

        CreateMap<Lesson, LessonDto>();
        CreateMap<CreateLessonDto, Lesson>();
        CreateMap<UpdateLessonDto, Lesson>();

        CreateMap<Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Payment>();
        CreateMap<UpdatePaymentDto, Payment>();

        CreateMap<Subscription, SubscriptionDto>();
        CreateMap<CreateSubscriptionDto, Subscription>();
        CreateMap<UpdateSubscriptionDto, Subscription>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}