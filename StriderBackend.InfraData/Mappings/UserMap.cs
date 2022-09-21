using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StriderBackend.Domain.Models;

namespace StriderBackend.InfraData.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(x => x.Id);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            //builder.HasMany(x => x.Followers)
            //    .WithOne(x => x)
            //    .HasForeignKey(x => x.Id);
        }
    }
}
