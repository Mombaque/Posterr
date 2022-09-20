
using AutoMapper;
using StriderBackend.Api.Controllers.V1.InputModels;
using StriderBackend.Domain.Models;

namespace StriderBackend.Api.Configuration.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            MapUsers();
            MapPosts();
        }

        private void MapPosts()
        {
            CreateMap<PostInputModel, Post>();
        }

        private void MapUsers()
        {
        }
    }
}
