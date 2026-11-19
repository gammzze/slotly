namespace Slotly.Domain.Enums;

public enum AppointmentStatus
{
    Pending = 0,
    Confirmed = 1,
    Rejected = 2,
    Completed = 3,
    NoShow = 4,
    CancelledByCustomer = 5,
    CancelledByOwner = 6,
    Expired = 7
}
