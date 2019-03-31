using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class PhotoshootConfigurations : IEntityTypeConfiguration<Photoshoot>
    {
        public void Configure(EntityTypeBuilder<Photoshoot> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(i => new { i.PhotographerId, i.PhotoshootTypeId })
                .IsUnique();

            builder.HasOne(photoshoot => photoshoot.Photographer)
                .WithMany(photographer => photographer.Photoshoots)
                .HasForeignKey(photoshoot => photoshoot.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(photoshoot => photoshoot.PhotoshootType)
                .WithMany(photoshootType => photoshootType.Photoshoots)
                .HasForeignKey(photoshoot => photoshoot.PhotoshootTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
