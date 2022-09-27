
using AutoMapper;
using Posterr.Api.Controllers.V1.InputModels;
using Posterr.Domain.Commands.User;
using Posterr.Domain.Repositories;

namespace Posterr.Api.Configuration.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
           MapPosts();
        }

        private void MapPosts()
        {
            CreateMap<SavePostInputModel, SavePostCommand>();
            CreateMap<GetPostsInputModel, GetPostsFilter>();
        }
    }
}
