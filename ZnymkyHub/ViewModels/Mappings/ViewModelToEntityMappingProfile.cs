using System;
using ZnymkyHub.DAL.Core.Domain;
using AutoMapper;
using ZnymkyHub.Models.Question;

namespace ZnymkyHub.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, User>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email))
                .ForMember(u => u.RegistrationDate, map => map.MapFrom(vm => DateTime.Now));
            CreateMap<RegistrationViewModel, Photographer>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email))
                .ForMember(u => u.RegistrationDate, map => map.MapFrom(vm => DateTime.Now));
            CreateMap<RegistrationViewModel, AuthorizedUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email))
                .ForMember(u => u.RegistrationDate, map => map.MapFrom(vm => DateTime.Now));
            CreateMap<QuestionDAO, Question>();
            CreateMap<Question, QuestionDAO>();
            CreateMap<AnswerDAO, Answer>();
            CreateMap<Answer, AnswerDAO>();
        }
    }
}
