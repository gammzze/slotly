using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(150);
        builder.Property(s => s.Description).HasMaxLength(1000);
        builder.Property(s => s.Price).HasPrecision(10, 2);

        builder.HasOne<Tenant>().WithMany().HasForeignKey(s => s.TenantId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(s => s.TenantId);
    }
}
