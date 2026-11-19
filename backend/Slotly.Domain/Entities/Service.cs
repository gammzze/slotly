using Slotly.Domain.Common;

namespace Slotly.Domain.Entities;

public class Service : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; private set; }
    public int BufferMinutes { get; set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }

    private Service() { }

    public Service(Guid tenantId, string name, int durationMinutes, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hizmet adı boş olamaz.", nameof(name));
        if (durationMinutes <= 0)
            throw new ArgumentOutOfRangeException(nameof(durationMinutes), "Süre sıfırdan büyük olmalı.");
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Fiyat negatif olamaz.");

        TenantId = tenantId;
        Name = name.Trim();
        DurationMinutes = durationMinutes;
        Price = price;
    }
}
