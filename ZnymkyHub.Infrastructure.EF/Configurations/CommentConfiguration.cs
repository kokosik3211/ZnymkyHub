
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(comment => comment.Photo)
                .WithMany(photo => photo.Comments)
                .HasForeignKey(comment => comment.PhotoId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
