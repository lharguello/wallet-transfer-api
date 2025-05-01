using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WalletTransfer.Api.Core.Entities;

namespace WalletTransfer.Api.Infrastructure.Data.EntityFramework.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.WalletId).IsRequired();
        builder.Property(t => t.Amount).HasColumnType("decimal(18, 2)");
        builder.Property(t => t.Type).HasConversion<string>().HasMaxLength(20);
        builder.Property(t => t.CreatedAt).IsRequired();

        builder.HasOne(t => t.Wallet).WithMany().HasForeignKey(t => t.WalletId).OnDelete(DeleteBehavior.Cascade);
    }
}
