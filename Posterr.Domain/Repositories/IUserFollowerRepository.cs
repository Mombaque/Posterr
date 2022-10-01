using Posterr.Domain.Models;

namespace Posterr.Domain.Repositories
{
    public interface IUserFollowerRepository
    {
        UserFollower GetUserFollower(int userId, int userFollowerId);
        void Add(UserFollower userFollowerEntity);
        void Delete(UserFollower? userFollowerEntity);
    }
}
