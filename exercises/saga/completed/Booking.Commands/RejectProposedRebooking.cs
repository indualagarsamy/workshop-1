namespace Booking.Commands
{
    using NServiceBus;

    public class RejectProposedRebooking :
        ICommand
    {
        public RejectProposedRebooking(string bookingReferenceId, string customerId)
        {
            BookingReferenceId = bookingReferenceId;
            CustomerId = customerId;
        }
        public string BookingReferenceId { get; }
        public string CustomerId { get; }
    }
}
