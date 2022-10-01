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
            MapPosts();
        }

        public void MapUsers()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.CreationDate, y => y.MapFrom(u => u.CreationDate.ToString("MMMM dd, yyyy")))
                .ForMember(x => x.PostsCount, y => y.MapFrom(u => u.Posts.Count));

            CreateMap<UserFollower, UserViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(u => u.Follower.Id))
                .ForMember(x => x.Name, y => y.MapFrom(u => u.Follower.Name))
                .ForMember(x => x.CreationDate, y => y.MapFrom(u => u.Follower.CreationDate));
        }
        private void MapPosts()
        {
            CreateMap<Post, PostViewModel>();
        }
    }
}
