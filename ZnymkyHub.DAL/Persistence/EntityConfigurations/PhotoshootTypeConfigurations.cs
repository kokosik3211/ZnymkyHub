using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class PhotoshootTypeConfigurations : IEntityTypeConfiguration<PhotoshootType>
    {
        public void Configure(EntityTypeBuilder<PhotoshootType> builder)
        {
            builder.HasKey(phType => phType.Id);
        }
    }
}
