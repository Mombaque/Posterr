
using AutoMapper;
using Posterr.Api.Controllers.V1.ViewModels;
using Posterr.Domain.Models;

namespace Posterr.Api.Configuration.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            MapUsers();
        }

        public void MapUsers()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<UserFollower, UserViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Follower.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Follower.Name));
        }
    }
}
