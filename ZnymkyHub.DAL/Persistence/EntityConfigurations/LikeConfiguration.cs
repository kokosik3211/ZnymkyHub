using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(p => new { p.Id, p.UserId, p.PhotoId });

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
