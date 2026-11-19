using Slotly.Domain.Common;

namespace Slotly.Domain.Entities;

public class Staff : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; set; }
    public Guid? UserId { get; set; }
    public string DisplayName { get; private set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }

    private Staff() { }

    public Staff(Guid tenantId, string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Çalışan adı boş olamaz.", nameof(displayName));

        TenantId = tenantId;
        DisplayName = displayName.Trim();
    }
}
