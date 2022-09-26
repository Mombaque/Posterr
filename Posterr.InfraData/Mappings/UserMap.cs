using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Models;

namespace Posterr.InfraData.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(x => x.Id);

            builder.HasMany(x => x.Followers)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
