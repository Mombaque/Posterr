
using AutoMapper;
using Posterr.Api.Controllers.V1.InputModels;
using Posterr.Domain.Commands.User;

namespace Posterr.Api.Configuration.AutoMapper
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
