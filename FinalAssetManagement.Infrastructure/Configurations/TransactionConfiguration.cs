using FinalAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FinalAssetManagement.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreatedAt)
                   .IsRequired();

            builder.Property(t => t.Description)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Amount)
                   .IsRequired()
                   .HasPrecision(18, 2);

            //--------------------------------------------------------

            builder.HasOne(t => t.Asset)
                   .WithMany(a => a.Transactions)
                   .HasForeignKey(t => t.AssetId)
                   .OnDelete(DeleteBehavior.Restrict);

            //--------------------------------------------------------
        }
    }
}
