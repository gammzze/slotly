using Microsoft.EntityFrameworkCore;
using Slotly.Domain.Entities;

namespace Slotly.Application.Common;

public interface IAppDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<Service> Services { get; }
    DbSet<Staff> Staff { get; }
    DbSet<StaffService> StaffServices { get; }
    DbSet<WorkingHour> WorkingHours { get; }
    DbSet<TimeOff> TimeOffs { get; }
    DbSet<Appointment> Appointments { get; }
    DbSet<Notification> Notifications { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
