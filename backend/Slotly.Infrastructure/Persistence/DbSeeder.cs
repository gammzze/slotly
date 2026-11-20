using Microsoft.EntityFrameworkCore;
using Slotly.Domain.Entities;
using Slotly.Domain.Enums;

namespace Slotly.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken ct = default)
    {
        if (await db.Tenants.AnyAsync(t => t.Slug == "ayse-guzellik", ct))
        {
            return;
        }

        var tenant = new Tenant("Ayşe Güzellik Salonu", "ayse-guzellik", BusinessCategory.BeautySalon)
        {
            Address = "Cumhuriyet Mah. 12, İzmit / Kocaeli",
            Phone = "0262 000 00 00",
            AutoConfirm = false
        };
        db.Tenants.Add(tenant);

        var services = new[]
        {
            new Service(tenant.Id, "Cilt Bakımı", 60, 900m) { BufferMinutes = 10, Description = "Derin temizlik ve nem bakımı" },
            new Service(tenant.Id, "Manikür", 45, 450m) { BufferMinutes = 5, Description = "Klasik el bakımı" },
            new Service(tenant.Id, "Kaş Şekillendirme", 30, 300m) { Description = "Kaş tasarımı" },
            new Service(tenant.Id, "Saç Bakımı", 90, 1200m) { BufferMinutes = 15, Description = "Keratin ve onarım" }
        };
        db.Services.AddRange(services);

        var elif = new Staff(tenant.Id, "Elif Demir");
        var merve = new Staff(tenant.Id, "Merve Kaya");
        db.Staff.AddRange(elif, merve);

        db.StaffServices.AddRange(
            new StaffService(tenant.Id, elif.Id, services[0].Id),
            new StaffService(tenant.Id, elif.Id, services[1].Id),
            new StaffService(tenant.Id, merve.Id, services[1].Id),
            new StaffService(tenant.Id, merve.Id, services[2].Id),
            new StaffService(tenant.Id, merve.Id, services[3].Id));

        var open = new TimeOnly(9, 0);
        var close = new TimeOnly(18, 0);
        var breakStart = new TimeOnly(12, 0);
        var breakEnd = new TimeOnly(13, 0);

        DayOfWeek[] workDays =
        [
            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
            DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday
        ];

        foreach (var staff in new[] { elif, merve })
        {
            foreach (var day in workDays)
            {
                db.WorkingHours.Add(new WorkingHour(
                    tenant.Id, staff.Id, day, open, close, breakStart, breakEnd));
            }
        }

        await db.SaveChangesAsync(ct);
    }
}
