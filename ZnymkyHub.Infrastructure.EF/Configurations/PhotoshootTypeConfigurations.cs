
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.Infrastructure.EF.Entities;

namespace ZnymkyHub.Infrastructure.EF.Configurations
{
    public class PhotoshootTypeConfigurations : IEntityTypeConfiguration<PhotoshootType>
    {
        public void Configure(EntityTypeBuilder<PhotoshootType> builder)
        {
            builder.HasKey(phType => phType.Id);
        }
    }
}
