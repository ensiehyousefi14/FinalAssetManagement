using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalAssetManagement.Infrastructure.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {

            builder.HasKey(a => a.Id);

            builder.Property(a => a.CreatedAt)
                   .IsRequired();

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.Price)
                   .IsRequired()
                   .HasPrecision(18, 2);

            //--------------------------------------------------------

            builder.HasOne(a => a.Category)
                   .WithMany(c => c.Assets)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); //Cannot delete CATEGORY that contains ASSETS

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Assets)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict); //Cannot delete USER that contains ASSETS

            //--------------------------------------------------------

        }
    }
}
