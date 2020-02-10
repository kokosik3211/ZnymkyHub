
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class PhotoResolutionConfigurations : IEntityTypeConfiguration<PhotoResolution>
    {
        public void Configure(EntityTypeBuilder<PhotoResolution> builder)
        {
            builder.HasKey(phResolution => phResolution.Id);
        }
    }
}
