using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class SavingConfiguration : IEntityTypeConfiguration<Saving>
    {
        public void Configure(EntityTypeBuilder<Saving> builder)
        {
            builder.HasKey(p => new { p.Id, p.UserId, p.PhotoId });

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
