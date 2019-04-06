using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class PhotographerOutgoingCityConfiguration : IEntityTypeConfiguration<PhotographerOutgoingCity>
    {
        public void Configure(EntityTypeBuilder<PhotographerOutgoingCity> builder)
        {
            builder.HasKey(p => new { p.PhotographerId, p.OutgoingCityId });

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(phCity => phCity.Photographer)
                .WithMany(ph => ph.PhotographerOutgoingCities)
                .HasForeignKey(phCity => phCity.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(phCity => phCity.OutgoingCity)
                .WithMany(city => city.PhotographerOutgoingCities)
                .HasForeignKey(phCity => phCity.OutgoingCityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
