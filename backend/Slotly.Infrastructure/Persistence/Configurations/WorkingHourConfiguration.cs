using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
{
    public void Configure(EntityTypeBuilder<WorkingHour> builder)
    {
        builder.ToTable("WorkingHours");
        builder.HasKey(w => w.Id);

        builder.HasOne<Staff>().WithMany().HasForeignKey(w => w.StaffId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(w => new { w.StaffId, w.DayOfWeek }).IsUnique();
        builder.HasIndex(w => w.TenantId);
    }
}
