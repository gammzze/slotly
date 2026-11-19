using Slotly.Domain.Enums;

namespace Slotly.Domain.Exceptions;

public class InvalidStateTransitionException : Exception
{
    public AppointmentStatus From { get; }
    public AppointmentStatus To { get; }

    public InvalidStateTransitionException(AppointmentStatus from, AppointmentStatus to)
        : base($"Geçersiz durum geçişi: {from} -> {to}.")
    {
        From = from;
        To = to;
    }
}
