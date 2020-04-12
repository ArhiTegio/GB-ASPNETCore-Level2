using AutoMapper;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Infrastructuse.AutoMapper
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<RegisterUserViewModel, User>()
                .ForMember(user => user.UserName, opt => opt.MapFrom(model => model.UserName))
                .ReverseMap();
        }
    }
}
