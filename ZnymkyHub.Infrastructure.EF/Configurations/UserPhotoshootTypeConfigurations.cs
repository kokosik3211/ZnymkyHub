
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class UserPhotoshootTypeConfigurations : IEntityTypeConfiguration<UserPhotoshootType>
    {
        public void Configure(EntityTypeBuilder<UserPhotoshootType> builder)
        {
            builder.HasKey(p => new { p.PhotographerId, p.PhotoshootTypeId });

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(userphType => userphType.Photographer)
                .WithMany(photographer => photographer.UserPhotoshootTypes)
                .HasForeignKey(userphType => userphType.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(userphType => userphType.PhotoshootType)
                .WithMany(photoshootType => photoshootType.UserPhotoshootTypes)
                .HasForeignKey(userphType => userphType.PhotoshootTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
