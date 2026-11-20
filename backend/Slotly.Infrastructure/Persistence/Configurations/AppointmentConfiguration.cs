using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slotly.Domain.Entities;

namespace Slotly.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status).HasConversion<string>().HasMaxLength(30);
        builder.Property(a => a.Note).HasMaxLength(1000);
        builder.Property(a => a.RejectionReason).HasMaxLength(500);
        builder.Property(a => a.IdempotencyKey).HasMaxLength(100);
        builder.Property(a => a.RowVersion).IsRowVersion();

        builder.HasOne<Tenant>().WithMany().HasForeignKey(a => a.TenantId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Service>().WithMany().HasForeignKey(a => a.ServiceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Staff>().WithMany().HasForeignKey(a => a.StaffId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.TenantId, a.StaffId, a.StartTime });
        builder.HasIndex(a => new { a.TenantId, a.Status, a.SeenAt });
        builder.HasIndex(a => new { a.CustomerId, a.StartTime });
        builder.HasIndex(a => new { a.TenantId, a.IdempotencyKey })
            .IsUnique()
            .HasFilter("[IdempotencyKey] IS NOT NULL");
    }
}
