using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletTransfer.Api.Core.Entities;

namespace WalletTransfer.Api.Infrastructure.Data.EntityFramework.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallets");

        builder.HasKey(w => w.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(w => w.DocumentId).IsRequired().HasMaxLength(50);
        builder.Property(w => w.Name).IsRequired().HasMaxLength(100);
        builder.Property(w => w.Balance).HasColumnType("decimal(18, 2)"); 
        builder.Property(w => w.CreatedAt).IsRequired();

        builder.HasIndex(w => w.DocumentId).IsUnique();
    }
}
