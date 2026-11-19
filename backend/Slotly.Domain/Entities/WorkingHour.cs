using Slotly.Domain.Common;

namespace Slotly.Domain.Entities;

public class WorkingHour : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; set; }
    public Guid StaffId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public TimeOnly? BreakStart { get; private set; }
    public TimeOnly? BreakEnd { get; private set; }

    private WorkingHour() { }

    public WorkingHour(Guid tenantId, Guid staffId, DayOfWeek dayOfWeek,
        TimeOnly startTime, TimeOnly endTime,
        TimeOnly? breakStart = null, TimeOnly? breakEnd = null)
    {
        if (endTime <= startTime)
            throw new ArgumentException("Kapanış saati açılıştan sonra olmalı.", nameof(endTime));

        if (breakStart.HasValue != breakEnd.HasValue)
            throw new ArgumentException("Mola başlangıcı ve bitişi birlikte verilmeli.");

        if (breakStart.HasValue && breakEnd.HasValue)
        {
            if (breakEnd <= breakStart)
                throw new ArgumentException("Mola bitişi başlangıçtan sonra olmalı.", nameof(breakEnd));
            if (breakStart < startTime || breakEnd > endTime)
                throw new ArgumentException("Mola, çalışma saatleri içinde olmalı.");
        }

        TenantId = tenantId;
        StaffId = staffId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        BreakStart = breakStart;
        BreakEnd = breakEnd;
    }
}
