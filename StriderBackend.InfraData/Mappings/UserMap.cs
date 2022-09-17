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

            builder.HasMany(x => x.Posts).WithOne();
        }
    }
}
