using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using WalletTransfer.Api.Core.Entities;
using WalletTransfer.Api.Infrastructure.Data.EntityFramework.Configurations;

namespace WalletTransfer.Api.Infrastructure.Data.EntityFramework;

public class ApplicationDBContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options,
    ILoggerFactory? loggerFactory
        ) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _loggerFactory = loggerFactory!;
    }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new TransactionConfiguration().Configure(builder.Entity<Transaction>());
        new WalletConfiguration().Configure(builder.Entity<Wallet>());

        base.OnModelCreating(builder);
        AdjustNamesForSqlStandard(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    /// <summary>
    /// change the names of the attributes and tables to the form: string_str_text
    /// </summary>
    /// <param name="modelBuilder">table models</param>
    private static void AdjustNamesForSqlStandard(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.ToSnakeCase());
            foreach (var property in entityType.GetProperties())
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasColumnName(property.Name.ToSnakeCase());
            }
        }
    }
}

static partial class NameConventions
{
    [GeneratedRegex("^_")]
    private static partial Regex UnderscoreRegex();

    [GeneratedRegex("(?:(?<l>[a-z0-9])(?<r>[A-Z])|(?<l>[A-Z])(?<r>[A-Z][a-z0-9]))")]
    private static partial Regex AlphanumericRegex();

    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var noLeadingUndescore = UnderscoreRegex().Replace(input, "");
        return AlphanumericRegex().Replace(noLeadingUndescore, "${l}_${r}").ToLower();
    }
}