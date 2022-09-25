
using AutoMapper;
using StriderBackend.Api.Controllers.V1.ViewModels;
using StriderBackend.Domain.Models;

namespace StriderBackend.Api.Configuration.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            MapUsers();
            MapPosts();
        }

        public void MapUsers()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<UserFollower, UserViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Follower.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Follower.Name));
        }
        private void MapPosts()
        {
            CreateMap<Post, PostViewModel>();

        }
    }
}
