using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalAssetManagement.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.Id);

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(128);

            //--------------------------------------------------------
        }
    }
}
