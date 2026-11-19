using Slotly.Domain.Common;

namespace Slotly.Domain.Entities;

public class StaffService : ITenantEntity
{
    public Guid StaffId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid TenantId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    private StaffService() { }

    public StaffService(Guid tenantId, Guid staffId, Guid serviceId)
    {
        TenantId = tenantId;
        StaffId = staffId;
        ServiceId = serviceId;
    }
}
