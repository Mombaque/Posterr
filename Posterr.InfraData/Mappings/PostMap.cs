using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posterr.Domain.Models;

namespace Posterr.InfraData.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post").HasKey(d => d.Id);

            //builder.HasOne(x => x.User)
            //    .WithMany(x => x.Posts)
            //    .HasForeignKey(x => x.UserId);
        }
    }
}
