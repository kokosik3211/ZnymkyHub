
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class SavingConfiguration : IEntityTypeConfiguration<Saving>
    {
        public void Configure(EntityTypeBuilder<Saving> builder)
        {
            builder.HasKey(p => new { p.UserId, p.PhotoId });

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(saving => saving.User)
                .WithMany(user => user.Savings)
                .HasForeignKey(saving => saving.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(saving => saving.Photo)
                .WithMany(photo => photo.Savings)
                .HasForeignKey(saving => saving.PhotoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
