using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).IsRequired().HasMaxLength(150);
        builder.Property(t => t.Slug).IsRequired().HasMaxLength(100);
        builder.HasIndex(t => t.Slug).IsUnique();

        builder.Property(t => t.Category).HasConversion<string>().HasMaxLength(30);
        builder.Property(t => t.Address).HasMaxLength(300);
        builder.Property(t => t.Phone).HasMaxLength(30);
        builder.Property(t => t.LogoUrl).HasMaxLength(500);
        builder.Property(t => t.BrandColor).HasMaxLength(7);
        builder.Property(t => t.TimeZone).IsRequired().HasMaxLength(50);
    }
}
