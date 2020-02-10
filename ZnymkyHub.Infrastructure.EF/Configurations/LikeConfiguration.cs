
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(p => new { p.UserId, p.PhotoId });

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(like => like.User)
                .WithMany(user => user.Likes)
                .HasForeignKey(like => like.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(like => like.Photo)
                .WithMany(photo => photo.Likes)
                .HasForeignKey(like => like.PhotoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
