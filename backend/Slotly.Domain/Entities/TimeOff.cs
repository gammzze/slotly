using Slotly.Domain.Common;

namespace Slotly.Domain.Entities;

public class TimeOff : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; set; }
    public Guid StaffId { get; private set; }
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public string? Reason { get; set; }

    private TimeOff() { }

    public TimeOff(Guid tenantId, Guid staffId, DateTimeOffset startDate, DateTimeOffset endDate, string? reason = null)
    {
        if (endDate < startDate)
            throw new ArgumentException("Bitiş tarihi başlangıçtan önce olamaz.", nameof(endDate));

        TenantId = tenantId;
        StaffId = staffId;
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason;
    }
}
