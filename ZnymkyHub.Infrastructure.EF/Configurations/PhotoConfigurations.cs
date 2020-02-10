
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class PhotoConfigurations : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(photo => photo.Photographer)
                .WithMany(user => user.Photos)
                .HasForeignKey(photo => photo.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(photo => photo.Photoshoot)
                .WithMany(phshoot => phshoot.Photos)
                .HasForeignKey(photo => photo.PhotoshootId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(photo => photo.PhotoshootType)
                .WithMany(phshoottype => phshoottype.Photos)
                .HasForeignKey(photo => photo.PhotoshootTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(photo => photo.PhotoResolution)
                .WithOne(phResolution => phResolution.Photo)
                .HasForeignKey<PhotoResolution>(phResolution => phResolution.PhotoId);
        }
    }
}
