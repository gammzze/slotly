using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Type).HasConversion<string>().HasMaxLength(50);
        builder.Property(n => n.Channel).HasConversion<string>().HasMaxLength(20);
        builder.Property(n => n.Status).HasConversion<string>().HasMaxLength(20);

        builder.HasOne<Tenant>().WithMany().HasForeignKey(n => n.TenantId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Appointment>().WithMany().HasForeignKey(n => n.AppointmentId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(n => new { n.Status, n.CreatedAt });
        builder.HasIndex(n => n.TenantId);
    }
}
