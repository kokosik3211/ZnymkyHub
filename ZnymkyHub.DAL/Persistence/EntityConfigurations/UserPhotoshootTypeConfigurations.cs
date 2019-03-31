using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class UserPhotoshootTypeConfigurations : IEntityTypeConfiguration<UserPhotoshootType>
    {
        public void Configure(EntityTypeBuilder<UserPhotoshootType> builder)
        {
            builder.HasKey(p => new {p.Id, p.PhotographerId, p.PhotoshootTypeId });

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
