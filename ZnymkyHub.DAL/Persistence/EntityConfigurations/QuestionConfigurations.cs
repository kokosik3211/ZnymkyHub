
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class QuestionConfigurations : IEntityTypeConfiguration<QuestionDAO>
    {
        public void Configure(EntityTypeBuilder<QuestionDAO> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd();
        }
    }
}
