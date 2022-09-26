using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Models;

namespace Posterr.InfraData.Mappings
{
    public class UserFollowerMap : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.ToTable("UserFollower").HasKey(x => x.Id);

            builder.HasOne(x => x.Follower)
                .WithOne()
                .HasForeignKey<UserFollower>(x => x.UserFollowerId)
                .HasPrincipalKey<User>(x => x.Id);
        }
    }
}
