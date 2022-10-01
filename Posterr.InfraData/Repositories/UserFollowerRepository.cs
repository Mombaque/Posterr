using Infra.Core;
using Posterr.Domain.Models;
using Posterr.Domain.Repositories;
using Posterr.InfraData.Context;

namespace Posterr.InfraData.Repositories
{
    public class UserFollowerRepository : Repository<UserFollower, Guid>, IUserFollowerRepository
    {
        private readonly DataContext _context;

        public UserFollowerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public UserFollower GetUserFollower(int userId, int userFollowerId)
        {
            return _context.UserFollower.FirstOrDefault(x =>
                x.UserId == userId &&
                x.UserFollowerId == userFollowerId);
        }
    }
}
