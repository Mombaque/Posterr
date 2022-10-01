
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
            MapUsers();
            MapPosts();
        }

        private void MapUsers()
        {
            CreateMap<FollowUserInputModel, FollowOrUnfollowUserCommand>();
        }

        private void MapPosts()
        {
            CreateMap<SavePostInputModel, SavePostCommand>()
                .ForMember(x => x.Date, y => y.MapFrom(_ => DateTime.Now));

            CreateMap<GetPostsInputModel, GetPostsFilter>()
                .ForMember(x => x.CurrentDate, y => y.MapFrom(_ => DateTime.Now));
        }
    }
}
