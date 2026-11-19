using Slotly.Domain.Common;
using Slotly.Domain.Enums;
using Slotly.Domain.Exceptions;

namespace Slotly.Domain.Entities;

public class Appointment : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; set; }
    public Guid CustomerId { get; private set; }
    public Guid ServiceId { get; private set; }
    public Guid StaffId { get; private set; }
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public string? Note { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTimeOffset? SeenAt { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
    public string? IdempotencyKey { get; private set; }

    private Appointment() { }

    public Appointment(
        Guid tenantId, Guid customerId, Guid serviceId, Guid staffId,
        DateTimeOffset startTime, DateTimeOffset endTime,
        bool autoConfirm, string? note = null, string? idempotencyKey = null)
    {
        if (startTime.Offset != TimeSpan.Zero || endTime.Offset != TimeSpan.Zero)
            throw new ArgumentException("Randevu saatleri UTC olarak verilmeli.");
        if (endTime <= startTime)
            throw new ArgumentException("Bitiş, başlangıçtan sonra olmalı.", nameof(endTime));

        TenantId = tenantId;
        CustomerId = customerId;
        ServiceId = serviceId;
        StaffId = staffId;
        StartTime = startTime;
        EndTime = endTime;
        Note = note;
        IdempotencyKey = idempotencyKey;
        Status = autoConfirm ? AppointmentStatus.Confirmed : AppointmentStatus.Pending;
    }

    public void MarkSeen(DateTimeOffset now)
    {
        SeenAt ??= now;
    }

    public void Confirm()
        => Transition(AppointmentStatus.Confirmed, AppointmentStatus.Pending);

    public void Reject(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Red nedeni zorunlu.", nameof(reason));

        Transition(AppointmentStatus.Rejected, AppointmentStatus.Pending);
        RejectionReason = reason.Trim();
    }

    public void Complete()
        => Transition(AppointmentStatus.Completed, AppointmentStatus.Confirmed);

    public void MarkNoShow()
        => Transition(AppointmentStatus.NoShow, AppointmentStatus.Confirmed);

    public void CancelByCustomer()
        => Transition(AppointmentStatus.CancelledByCustomer,
            AppointmentStatus.Pending, AppointmentStatus.Confirmed);

    public void CancelByOwner()
        => Transition(AppointmentStatus.CancelledByOwner, AppointmentStatus.Confirmed);

    public void Expire()
        => Transition(AppointmentStatus.Expired, AppointmentStatus.Pending);

    private void Transition(AppointmentStatus to, params AppointmentStatus[] allowedFrom)
    {
        if (!allowedFrom.Contains(Status))
            throw new InvalidStateTransitionException(Status, to);

        Status = to;
    }
}
