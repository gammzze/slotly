using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class StaffServiceConfiguration : IEntityTypeConfiguration<StaffService>
{
    public void Configure(EntityTypeBuilder<StaffService> builder)
    {
        builder.ToTable("StaffServices");
        builder.HasKey(x => new { x.StaffId, x.ServiceId });

        builder.HasOne<Staff>().WithMany().HasForeignKey(x => x.StaffId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Service>().WithMany().HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.TenantId);
    }
}
