using Slotly.Domain.Entities;
using Slotly.Domain.Enums;
using Slotly.Domain.Exceptions;

namespace Slotly.UnitTests.Domain;

public class AppointmentStateTests
{
    private static Appointment Create(bool autoConfirm)
    {
        var start = new DateTimeOffset(2026, 12, 16, 10, 0, 0, TimeSpan.Zero);
        return new Appointment(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            start, start.AddHours(1), autoConfirm);
    }

    [Fact]
    public void New_appointment_is_pending_when_auto_confirm_is_off()
    {
        var appointment = Create(autoConfirm: false);

        Assert.Equal(AppointmentStatus.Pending, appointment.Status);
    }

    [Fact]
    public void New_appointment_is_confirmed_when_auto_confirm_is_on()
    {
        var appointment = Create(autoConfirm: true);

        Assert.Equal(AppointmentStatus.Confirmed, appointment.Status);
    }

    [Fact]
    public void Pending_can_be_confirmed()
    {
        var appointment = Create(autoConfirm: false);

        appointment.Confirm();

        Assert.Equal(AppointmentStatus.Confirmed, appointment.Status);
    }

    [Fact]
    public void Rejected_appointment_cannot_be_confirmed()
    {
        var appointment = Create(autoConfirm: false);
        appointment.Reject("Müsait değiliz");

        Assert.Throws<InvalidStateTransitionException>(() => appointment.Confirm());
    }

    [Fact]
    public void Cancelled_appointment_cannot_be_confirmed()
    {
        var appointment = Create(autoConfirm: false);
        appointment.CancelByCustomer();

        Assert.Throws<InvalidStateTransitionException>(() => appointment.Confirm());
    }

    [Fact]
    public void Pending_cannot_be_completed()
    {
        var appointment = Create(autoConfirm: false);

        Assert.Throws<InvalidStateTransitionException>(() => appointment.Complete());
    }

    [Fact]
    public void Completed_is_a_final_state()
    {
        var appointment = Create(autoConfirm: true);
        appointment.Complete();

        Assert.Throws<InvalidStateTransitionException>(() => appointment.CancelByOwner());
    }

    [Fact]
    public void Reject_requires_a_reason()
    {
        var appointment = Create(autoConfirm: false);

        Assert.Throws<ArgumentException>(() => appointment.Reject("  "));
    }

    [Fact]
    public void Non_utc_times_are_rejected()
    {
        var start = new DateTimeOffset(2026, 12, 16, 10, 0, 0, TimeSpan.FromHours(3));

        Assert.Throws<ArgumentException>(() => new Appointment(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            start, start.AddHours(1), autoConfirm: false));
    }
}
