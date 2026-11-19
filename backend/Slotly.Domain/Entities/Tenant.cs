using Slotly.Domain.Common;
using Slotly.Domain.Enums;

namespace Slotly.Domain.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public BusinessCategory Category { get; private set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? LogoUrl { get; set; }
    public string? BrandColor { get; set; }
    public string TimeZone { get; set; } = "Europe/Istanbul";
    public int SlotIntervalMinutes { get; set; } = 30;
    public int BookingWindowDays { get; set; } = 60;
    public int MinNoticeHours { get; set; } = 2;
    public int CancellationHours { get; set; } = 24;
    public bool AutoConfirm { get; set; }
    public bool IsDeleted { get; set; }

    private Tenant() { }

    public Tenant(string name, string slug, BusinessCategory category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("İşletme adı boş olamaz.", nameof(name));
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug boş olamaz.", nameof(slug));

        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Category = category;
    }
}
