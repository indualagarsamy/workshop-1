namespace Booking.Events
{
    using NServiceBus;

    public class RebookingProposalWasRejected :
        IEvent
    {
        public RebookingProposalWasRejected(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
