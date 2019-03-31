using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class PhotoResolutionConfigurations : IEntityTypeConfiguration<PhotoResolution>
    {
        public void Configure(EntityTypeBuilder<PhotoResolution> builder)
        {
            builder.HasKey(phResolution => phResolution.Id);
        }
    }
}
