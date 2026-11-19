using Slotly.Domain.Common;
using Slotly.Domain.Enums;

namespace Slotly.Domain.Entities;

public class Notification : BaseEntity, ITenantEntity
{
    public Guid TenantId { get; set; }
    public Guid UserId { get; private set; }
    public Guid? AppointmentId { get; private set; }
    public NotificationType Type { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public string? Payload { get; private set; }
    public NotificationStatus Status { get; private set; } = NotificationStatus.Pending;
    public int RetryCount { get; private set; }
    public DateTimeOffset? ProcessedAt { get; private set; }

    private Notification() { }

    public Notification(
        Guid tenantId, Guid userId, NotificationType type, NotificationChannel channel,
        Guid? appointmentId = null, string? payload = null)
    {
        TenantId = tenantId;
        UserId = userId;
        Type = type;
        Channel = channel;
        AppointmentId = appointmentId;
        Payload = payload;
    }

    public void MarkSent(DateTimeOffset now)
    {
        Status = NotificationStatus.Sent;
        ProcessedAt = now;
    }

    public void MarkFailedAttempt(int maxRetries, DateTimeOffset now)
    {
        RetryCount++;
        if (RetryCount >= maxRetries)
        {
            Status = NotificationStatus.Failed;
            ProcessedAt = now;
        }
    }
}
