
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
        }

        public void MapPosts()
        {
            CreateMap<Post, PostViewModel>();
                //.ForMember(x => x.Content, y => y.MapFrom(z => ));
        }
    }
}
