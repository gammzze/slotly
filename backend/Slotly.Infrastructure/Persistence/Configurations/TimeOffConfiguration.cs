using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class TimeOffConfiguration : IEntityTypeConfiguration<TimeOff>
{
    public void Configure(EntityTypeBuilder<TimeOff> builder)
    {
        builder.ToTable("TimeOffs");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Reason).HasMaxLength(300);

        builder.HasOne<Staff>().WithMany().HasForeignKey(t => t.StaffId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(t => new { t.StaffId, t.StartDate, t.EndDate });
        builder.HasIndex(t => t.TenantId);
    }
}
