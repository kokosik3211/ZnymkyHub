using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class OutgoingCityConfigurations : IEntityTypeConfiguration<OutgoingCity>
    {
        public void Configure(EntityTypeBuilder<OutgoingCity> builder)
        {
            builder.HasKey(outgoingCity => outgoingCity.Id);
        }
    }
}
