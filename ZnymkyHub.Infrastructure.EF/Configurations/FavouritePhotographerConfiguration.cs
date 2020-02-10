
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class FavouritePhotographerConfiguration : IEntityTypeConfiguration<FavouritePhotographer>
    {
        public void Configure(EntityTypeBuilder<FavouritePhotographer> builder)
        {
            builder.HasKey(p => new { p.UserId, p.PhotographerId });

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(fp => fp.User)
                .WithMany(user => user.FavouritePhotographerPhotographers)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fp => fp.Photographer)
                .WithMany(ph => ph.FavouritePhotographerUsers)
                .HasForeignKey(fp => fp.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
