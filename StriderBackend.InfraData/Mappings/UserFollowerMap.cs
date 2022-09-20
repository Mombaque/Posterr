using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StriderBackend.Domain.Models;

namespace StriderBackend.InfraData.Mappings
{
    public class UserFollowerMap : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.ToTable("UserFollower").HasKey(x => new { x.UserId, x.UserFollowerId });
        }
    }
}
