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
            CreateMap<RegistrationViewModel, User>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            CreateMap<QuestionDAO, Question>();
            CreateMap<Question, QuestionDAO>();
            CreateMap<AnswerDAO, Answer>();
            CreateMap<Answer, AnswerDAO>();
        }
    }
}
