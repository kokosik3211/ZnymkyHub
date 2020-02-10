
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class OutgoingCityConfigurations : IEntityTypeConfiguration<OutgoingCity>
    {
        public void Configure(EntityTypeBuilder<OutgoingCity> builder)
        {
            builder.HasKey(outgoingCity => outgoingCity.Id);
        }
    }
}
