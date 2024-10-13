using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCloudServer.Services.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired()
            .HasMaxLength(36);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(15); 

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(256);
    }
}