using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence.EntityConfigurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            var genderConverter = new ValueConverter<Gender, string>(
                v => v.ToString(),
                v => (Gender)Enum.Parse(typeof(Gender), v));

            builder.Property(user => user.Gender)
                .HasConversion(genderConverter);
        }
    }
}