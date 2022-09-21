
using AutoMapper;
using StriderBackend.Api.Controllers.V1.InputModels;
using StriderBackend.Domain.Commands.User;

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
            CreateMap<SavePostInputModel, SavePostCommand>();
        }

        private void MapUsers()
        {
        }
    }
}
